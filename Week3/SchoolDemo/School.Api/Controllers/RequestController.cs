using Microsoft.AspNetCore.Mvc;
using Serilog;

using System.Net.Http;
using System.Net.Http.Json;

namespace School.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // "/requests
    public class RequestController : ControllerBase
    {
        // Fields
        private readonly ILogger<RequestController> _logger;

        private HttpClient client = new HttpClient();

        string url = "https://jsonplaceholder.typicode.com/posts/";

        // Constructor
        public RequestController(ILogger<RequestController> logger)
        {
            _logger = logger;
        }

        // Methods

        // Get All Posts
        [HttpGet("posts", Name = "GetAllPosts")]
        // "requests/posts"
        public async Task<IActionResult> GetAllPostsAsync()
        {
            _logger.LogInformation("Getting all posts");

            var results = await client.GetAsync(url);

            Console.WriteLine(results.Content.ReadAsStringAsync().Result);

            // var PostList = await results.Content.ReadFromJsonAsync<List<PostDTO>>();

            return Ok(results.Content.ReadAsStringAsync().Result);
        }

/* Let's play with some error handling. I've updated the Instructor controller to be more robust, and a hopefully cleaner example.
*  So let's look into differnet ways to use throwing exceptions to handle errors and modify the control-flow of the application.
*  We'd discussed the try-catch-finally pattern, so the first thing to understand is what is thrown by an exception.
*  The exception is an object that is thrown when something goes wrong in the application. We can catch it and handle it, 
*  preventing it from crashing the application and stopping it. Depending on what went wrong, we'll get different types of exceptions.
*  Having a loop that tries to iterate past the end of a list will generate an "IndexOutOfRangeException".
*  A null reference will generate a "NullReferenceException".
*  A divide by zero will generate a "DivideByZeroException".
*  A file that doesn't exist will generate a "FileNotFoundException".
*  Basically, anything that is common enough that we know how to identify it will generate an exception, and we can catch it.
*
*  Imagine you've got a method/behavior that is just not working. The exception SHOULD give us some insight into what is going on.
*  If the exception is something we know how to handle, we can catch it and modify the control flow of the application. */

        [HttpGet("error/1", Name = "GetError")]
        // "requests/error/1"
        public async Task<IActionResult> ThrowAnException()
        {
            throw new Exception("This is an error");
            /* ok, not the most elegant example, but it should let us see what happens when we throw an exception!
            *  give it a shot and see what happens! 
            *  does the application crash, burst into flames, or does it continue to run?
            *  Why do you think it does that? */
        }

        [HttpPost("error/2", Name = "GetError2")]
        // "requests/error/2"
        public async Task<IActionResult> CauseAnException(int[] numbers)
        {
            // Let's do some thing that will break the application
            // this method will sum up some number, but only 5.
            // Try it out first! I've even provided an array of values that will work.
            // [ 1, 2, 3, 4, 5 ]

            int sum = 0;
            for (int i = 0; i < 5; i++)
            {
                sum += numbers[i];
            }
            return Ok(sum);

            // After you try it with 5 number, try 7.
            // Then try 2.
            // What happens? How can we prevent it from sending failure responses to the client?
        }

        /* Ok, so we know that certain things can cuase exceptions, and if we want to we can create them ourselves.
        * So let's look at how we handle them when they do occur.
        */
        
        [HttpGet("error/3", Name = "GetError3")]
        // "requests/error/3"
        public async Task<IActionResult> HandleAnException()
        {
            // we'll start small
            // there's two bits to this - What do you think MIGHT cause the problems, and what to do if it does?
            try // keep the dangerous stuff here. Anything that COULD cause an exception
            {
                throw new Exception("This is an error");
            }
            catch (Exception ex) // and the catch block is where we handle the event. 
            // Pay attention to the response object that you get from the Get request you make.
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("error/4/{a}", Name = "GetError4")]
        // "requests/error/4/{a}"
        public async Task<IActionResult> HandleAnException2(int a = 0)
        {
            // You're not limited to just one catch block. You can have as many as you want, each catching different types of exceptions.
            try
            {
                switch(a) // based on the value of a, throw a different type of exception
                {
                    case 1:
                        throw new DivideByZeroException("This is an error");
                    case 2:
                        throw new IndexOutOfRangeException("This is an error");
                    case 3:
                        throw new NullReferenceException("This is an error");
                    default:
                        throw new Exception("This is an error"); // But you better believe we're throwing SOMETHING!
                };                
            }

            // To handle different exceptions, we need to list them in order of most specific to least specific.
            // Some exceptions are more specific than others, so we want to handle the more specific ones first.
            catch (DivideByZeroException ex)
            {
                return BadRequest("Cannot divide by zero");
            }
            catch (IndexOutOfRangeException ex)
            {
                return BadRequest("Index out of range");
            }
            catch (NullReferenceException ex)
            {
                return BadRequest("That was literally nothing");
            }

            // Then we can handle the more general ones, or use the catch-all of "Exception"
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("error/finally", Name = "ErrorFinally")]
        // "requests/error/finally"
        public async Task<IActionResult> HandleFinally()
        {
            /* Let's look at something that uses the finally block
            *  Try runing this at least twice - as is, then comment out
            *  the 'throw new Exception("This is an error");' 
            *  line and run it again. What happens?
            *
            *  After you give that a try, check out what happens when you try to 
            *  return the response object from the catch or finally block.
            *  The lines are there for you, but commented out. What happens? */

            var response = "Starting at the top \n";
            try
            {
                // something that causes an exception
                throw new Exception("This is an error");
                response += "Made it to the end of the try block! \n";
                return Ok(response);
            }
            catch (Exception ex)
            {
                // something that will run if the exception is caught
                response += "Made it to the catch block! \n";

                // return BadRequest(ex.Message);
            }
            finally
            {
                // something that will run whether or not the exception is caught
                response += "Made it to the finally block! \n";

                // return Ok(response);
            }

            return Ok(response);
        }

        /* Ok, that should give us lots of flexibility in how we handle exceptions,
        *  and we can even use the finally block to do some cleanup. There's also
        *  testing to consider. Anything that we put into our methods, and especially
        *  any control flow that may branch the execution path, we need to test.
        *  Here's a quick example from the Student Service tests:

        [Fact]
        public async Task CreateAsync_WithNullStudent_ShouldThrowException()
        {
            // Arrange
            Student nullStudent = null;
            _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Student>()))
                          .ThrowsAsync(new ArgumentNullException());

            // This bit of Arrange sets up a mock repository that will throw an exception
            // when it's asked to add a student. This is a good way to simulate a situation
            // where we expect the repository to throw an exception, but that we don't have
            // a better way to describe off the top of our heads.

            // Act & Assert
            await _service.Invoking(s => s.CreateAsync(nullStudent))
                         .Should().ThrowAsync<ArgumentNullException>();

            // So when the service is asked to create a student, and the student is null,
            // it should throw an ArgumentNullException. Our testing can verify the type of
            // exception thrown, and we could drill down into the excpetion object and message
            // if we wanted to. 

            _mockRepository.Verify(repo => repo.AddAsync(nullStudent), Times.Once);
        }


        *  Remember that testing is about validating the happy path (your normal everyday
        *  flow, where everything is working as expected), and the unhappy path (where
        *  something goes wrong). This definitely falls under testing the unhappy path,
        *  but it's an important part of proving that our application is robust and
        *  can handle all the possiblities! */
    }
}