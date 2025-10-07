-- french_tutor seed script (idempotent)
-- Target DB: FrenchTutor2Db
-- Tables expected: Artists(Id, Name, Country, Bio), Songs(Id, Title, Year, Lyrics, Translation, ArtistId),
--                  Terms(Id, French, English, Notes), SongTerm(SongsId, TermsId)

SET NOCOUNT ON;

-- ---------- Safety checks ----------
IF OBJECT_ID('dbo.Artists') IS NULL OR OBJECT_ID('dbo.Songs') IS NULL
BEGIN
    PRINT 'Required tables not found. Make sure EF migrations have been applied.';
    RETURN;
END

-- ---------- Artists ----------
IF NOT EXISTS (SELECT 1 FROM Artists WHERE Name = N'Édith Piaf')
INSERT INTO Artists (Name, Country, Bio) VALUES (N'Édith Piaf', N'France', N'Iconic French chanteuse and cultural symbol.');

IF NOT EXISTS (SELECT 1 FROM Artists WHERE Name = N'Jacques Brel')
INSERT INTO Artists (Name, Country, Bio) VALUES (N'Jacques Brel', N'Belgium', N'Poet of the chanson; intense, theatrical delivery.');

IF NOT EXISTS (SELECT 1 FROM Artists WHERE Name = N'Georges Brassens')
INSERT INTO Artists (Name, Country, Bio) VALUES (N'Georges Brassens', N'France', N'Wry, literate songwriter with a guitar and wit.');

IF NOT EXISTS (SELECT 1 FROM Artists WHERE Name = N'Charles Aznavour')
INSERT INTO Artists (Name, Country, Bio) VALUES (N'Charles Aznavour', N'France', N'Chanson legend blending drama and tenderness.');

IF NOT EXISTS (SELECT 1 FROM Artists WHERE Name = N'Joe Dassin')
INSERT INTO Artists (Name, Country, Bio) VALUES (N'Joe Dassin', N'France', N'American-French singer of sunny 60s/70s pop.');

IF NOT EXISTS (SELECT 1 FROM Artists WHERE Name = N'Françoise Hardy')
INSERT INTO Artists (Name, Country, Bio) VALUES (N'Françoise Hardy', N'France', N'Yé-yé icon with introspective, cool delivery.');

IF NOT EXISTS (SELECT 1 FROM Artists WHERE Name = N'Yves Montand')
INSERT INTO Artists (Name, Country, Bio) VALUES (N'Yves Montand', N'France', N'Charming actor-singer; classic Parisian flavor.');

IF NOT EXISTS (SELECT 1 FROM Artists WHERE Name = N'Serge Gainsbourg')
INSERT INTO Artists (Name, Country, Bio) VALUES (N'Serge Gainsbourg', N'France', N'Provocateur and wordplay master of chanson-pop.');

IF NOT EXISTS (SELECT 1 FROM Artists WHERE Name = N'Barbara')
INSERT INTO Artists (Name, Country, Bio) VALUES (N'Barbara', N'France', N'Deeply personal, poetic singer-songwriter.');

IF NOT EXISTS (SELECT 1 FROM Artists WHERE Name = N'Michel Sardou')
INSERT INTO Artists (Name, Country, Bio) VALUES (N'Michel Sardou', N'France', N'Big-voiced singer of emotive 70s hits.');

IF NOT EXISTS (SELECT 1 FROM Artists WHERE Name = N'Michel Polnareff')
INSERT INTO Artists (Name, Country, Bio) VALUES (N'Michel Polnareff', N'France', N'Baroque pop craftsman with lush melodies.');

IF NOT EXISTS (SELECT 1 FROM Artists WHERE Name = N'France Gall')
INSERT INTO Artists (Name, Country, Bio) VALUES (N'France Gall', N'France', N'Sparkling yé-yé star; Eurovision winner.');

IF NOT EXISTS (SELECT 1 FROM Artists WHERE Name = N'Dalida')
INSERT INTO Artists (Name, Country, Bio) VALUES (N'Dalida', N'France', N'Egyptian-Italian-French icon of timeless pop.');

IF NOT EXISTS (SELECT 1 FROM Artists WHERE Name = N'Charles Trenet')
INSERT INTO Artists (Name, Country, Bio) VALUES (N'Charles Trenet', N'France', N'The ‘‘singing fool’’; jaunty, poetic imagery.');

-- ---------- Songs (lyrics null; translation is a summary) ----------
IF NOT EXISTS (SELECT 1 FROM Songs WHERE Title = N'La vie en rose')
INSERT INTO Songs (Title, Year, Lyrics, Translation, ArtistId)
VALUES (N'La vie en rose', 1947, NULL, N'A romantic declaration: love makes everything glow; life turns rosy in a lover''s arms.', (SELECT Id FROM Artists WHERE Name=N'Édith Piaf'));

IF NOT EXISTS (SELECT 1 FROM Songs WHERE Title = N'Non, je ne regrette rien')
INSERT INTO Songs (Title, Year, Lyrics, Translation, ArtistId)
VALUES (N'Non, je ne regrette rien', 1960, NULL, N'A bold reset: no regrets about the past; only the present love matters.', (SELECT Id FROM Artists WHERE Name=N'Édith Piaf'));

IF NOT EXISTS (SELECT 1 FROM Songs WHERE Title = N'Hymne à l’amour')
INSERT INTO Songs (Title, Year, Lyrics, Translation, ArtistId)
VALUES (N'Hymne à l’amour', 1950, NULL, N'A vow of limitless devotion, promising to face any trial for love.', (SELECT Id FROM Artists WHERE Name=N'Édith Piaf'));

IF NOT EXISTS (SELECT 1 FROM Songs WHERE Title = N'Ne me quitte pas')
INSERT INTO Songs (Title, Year, Lyrics, Translation, ArtistId)
VALUES (N'Ne me quitte pas', 1959, NULL, N'A desperate plea to a departing lover, promising wonders to win them back.', (SELECT Id FROM Artists WHERE Name=N'Jacques Brel'));

IF NOT EXISTS (SELECT 1 FROM Songs WHERE Title = N'Amsterdam')
INSERT INTO Songs (Title, Year, Lyrics, Translation, ArtistId)
VALUES (N'Amsterdam', 1964, NULL, N'A gritty portrait of sailors in port—excess, yearning, and raw humanity.', (SELECT Id FROM Artists WHERE Name=N'Jacques Brel'));

IF NOT EXISTS (SELECT 1 FROM Songs WHERE Title = N'Les copains d’abord')
INSERT INTO Songs (Title, Year, Lyrics, Translation, ArtistId)
VALUES (N'Les copains d’abord', 1964, NULL, N'A celebration of loyal friendship; a boat named ‘‘Friends First’’ symbolizes camaraderie.', (SELECT Id FROM Artists WHERE Name=N'Georges Brassens'));

IF NOT EXISTS (SELECT 1 FROM Songs WHERE Title = N'La mauvaise réputation')
INSERT INTO Songs (Title, Year, Lyrics, Translation, ArtistId)
VALUES (N'La mauvaise réputation', 1952, NULL, N'Defiant satire of social judgment; a stubborn nonconformist keeps his course.', (SELECT Id FROM Artists WHERE Name=N'Georges Brassens'));

IF NOT EXISTS (SELECT 1 FROM Songs WHERE Title = N'La Bohème')
INSERT INTO Songs (Title, Year, Lyrics, Translation, ArtistId)
VALUES (N'La Bohème', 1965, NULL, N'Bittersweet nostalgia for starving-artist youth in Montmartre, rich in memories.', (SELECT Id FROM Artists WHERE Name=N'Charles Aznavour'));

IF NOT EXISTS (SELECT 1 FROM Songs WHERE Title = N'Les Champs-Élysées')
INSERT INTO Songs (Title, Year, Lyrics, Translation, ArtistId)
VALUES (N'Les Champs-Élysées', 1969, NULL, N'A carefree stroll on Paris’ avenue; chance meetings and sunny optimism.', (SELECT Id FROM Artists WHERE Name=N'Joe Dassin'));

IF NOT EXISTS (SELECT 1 FROM Songs WHERE Title = N'Tous les garçons et les filles')
INSERT INTO Songs (Title, Year, Lyrics, Translation, ArtistId)
VALUES (N'Tous les garçons et les filles', 1962, NULL, N'Lonely youth observing couples; longing for love’s first true connection.', (SELECT Id FROM Artists WHERE Name=N'Françoise Hardy'));

IF NOT EXISTS (SELECT 1 FROM Songs WHERE Title = N'Sous le ciel de Paris')
INSERT INTO Songs (Title, Year, Lyrics, Translation, ArtistId)
VALUES (N'Sous le ciel de Paris', 1951, NULL, N'A poetic ode to Paris’ sky and the small dramas of life beneath it.', (SELECT Id FROM Artists WHERE Name=N'Yves Montand'));

IF NOT EXISTS (SELECT 1 FROM Songs WHERE Title = N'La Javanaise')
INSERT INTO Songs (Title, Year, Lyrics, Translation, ArtistId)
VALUES (N'La Javanaise', 1963, NULL, N'Playful word-jazz and sensuality; a slow dance of love and language.', (SELECT Id FROM Artists WHERE Name=N'Serge Gainsbourg'));

IF NOT EXISTS (SELECT 1 FROM Songs WHERE Title = N'L’Aigle noir')
INSERT INTO Songs (Title, Year, Lyrics, Translation, ArtistId)
VALUES (N'L’Aigle noir', 1970, NULL, N'A dream-vision of a black eagle; memory, trauma, and rebirth intertwine.', (SELECT Id FROM Artists WHERE Name=N'Barbara'));

IF NOT EXISTS (SELECT 1 FROM Songs WHERE Title = N'La Maladie d’amour')
INSERT INTO Songs (Title, Year, Lyrics, Translation, ArtistId)
VALUES (N'La Maladie d’amour', 1973, NULL, N'Love portrayed as a ‘‘sickness’’ carried through generations, catchy and grand.', (SELECT Id FROM Artists WHERE Name=N'Michel Sardou'));

IF NOT EXISTS (SELECT 1 FROM Songs WHERE Title = N'Love Me, Please Love Me')
INSERT INTO Songs (Title, Year, Lyrics, Translation, ArtistId)
VALUES (N'Love Me, Please Love Me', 1966, NULL, N'Vulnerable confession: pleading for reciprocated love with orchestral sweep.', (SELECT Id FROM Artists WHERE Name=N'Michel Polnareff'));

IF NOT EXISTS (SELECT 1 FROM Songs WHERE Title = N'Poupée de cire, poupée de son')
INSERT INTO Songs (Title, Year, Lyrics, Translation, ArtistId)
VALUES (N'Poupée de cire, poupée de son', 1965, NULL, N'A pop idol questions being molded by others, seeking an authentic voice.', (SELECT Id FROM Artists WHERE Name=N'France Gall'));

IF NOT EXISTS (SELECT 1 FROM Songs WHERE Title = N'Paroles, paroles')
INSERT INTO Songs (Title, Year, Lyrics, Translation, ArtistId)
VALUES (N'Paroles, paroles', 1973, NULL, N'A duet about empty promises; one partner is tired of charming talk.', (SELECT Id FROM Artists WHERE Name=N'Dalida'));

IF NOT EXISTS (SELECT 1 FROM Songs WHERE Title = N'La mer')
INSERT INTO Songs (Title, Year, Lyrics, Translation, ArtistId)
VALUES (N'La mer', 1946, NULL, N'A shimmering hymn to the sea—ever-changing light, motion, and joy.', (SELECT Id FROM Artists WHERE Name=N'Charles Trenet'));

-- ---------- Terms ----------
IF OBJECT_ID('dbo.Terms') IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Terms WHERE French=N'amour')
    INSERT INTO Terms (French, English, Notes) VALUES (N'amour', N'love', N'noun');

    IF NOT EXISTS (SELECT 1 FROM Terms WHERE French=N'cœur')
    INSERT INTO Terms (French, English, Notes) VALUES (N'cœur', N'heart', N'noun; figurative feelings');

    IF NOT EXISTS (SELECT 1 FROM Terms WHERE French=N'regret')
    INSERT INTO Terms (French, English, Notes) VALUES (N'regret', N'regret', N'noun/verb idea');

    IF NOT EXISTS (SELECT 1 FROM Terms WHERE French=N'rose')
    INSERT INTO Terms (French, English, Notes) VALUES (N'rose', N'pink/rose', N'color / rose imagery');

    IF NOT EXISTS (SELECT 1 FROM Terms WHERE French=N'ciel')
    INSERT INTO Terms (French, English, Notes) VALUES (N'ciel', N'sky', N'noun; poetic usage');

    IF NOT EXISTS (SELECT 1 FROM Terms WHERE French=N'mer')
    INSERT INTO Terms (French, English, Notes) VALUES (N'mer', N'sea', N'noun; natural imagery');

    IF NOT EXISTS (SELECT 1 FROM Terms WHERE French=N'ami')
    INSERT INTO Terms (French, English, Notes) VALUES (N'ami', N'friend', N'masc.; amie = fem.');

    IF NOT EXISTS (SELECT 1 FROM Terms WHERE French=N'paroles')
    INSERT INTO Terms (French, English, Notes) VALUES (N'paroles', N'words', N'lyrics/empty talk');

    IF NOT EXISTS (SELECT 1 FROM Terms WHERE French=N'souvenir')
    INSERT INTO Terms (French, English, Notes) VALUES (N'souvenir', N'memory', N'noun; remembrance');

    IF NOT EXISTS (SELECT 1 FROM Terms WHERE French=N'tristesse')
    INSERT INTO Terms (French, English, Notes) VALUES (N'tristesse', N'sadness', N'noun; emotion');

    IF NOT EXISTS (SELECT 1 FROM Terms WHERE French=N'joie')
    INSERT INTO Terms (French, English, Notes) VALUES (N'joie', N'joy', N'noun; emotion');

    IF NOT EXISTS (SELECT 1 FROM Terms WHERE French=N'liberté')
    INSERT INTO Terms (French, English, Notes) VALUES (N'liberté', N'freedom', N'noun; concept');
END

-- ---------- Link terms to select songs (if join table exists) ----------
IF OBJECT_ID('dbo.SongTerm') IS NOT NULL
BEGIN
    -- La vie en rose: amour, rose, joie
    IF NOT EXISTS (SELECT 1 FROM SongTerm st
                   JOIN Songs s ON s.Id = st.SongsId
                   JOIN Terms t ON t.Id = st.TermsId
                   WHERE s.Title = N'La vie en rose' AND t.French = N'amour')
    INSERT INTO SongTerm (SongsId, TermsId)
    SELECT s.Id, t.Id FROM Songs s CROSS JOIN Terms t
    WHERE s.Title=N'La vie en rose' AND t.French=N'amour';

    IF NOT EXISTS (SELECT 1 FROM SongTerm st
                   JOIN Songs s ON s.Id = st.SongsId
                   JOIN Terms t ON t.Id = st.TermsId
                   WHERE s.Title = N'La vie en rose' AND t.French = N'rose')
    INSERT INTO SongTerm (SongsId, TermsId)
    SELECT s.Id, t.Id FROM Songs s CROSS JOIN Terms t
    WHERE s.Title=N'La vie en rose' AND t.French=N'rose';

    IF NOT EXISTS (SELECT 1 FROM SongTerm st
                   JOIN Songs s ON s.Id = st.SongsId
                   JOIN Terms t ON t.Id = st.TermsId
                   WHERE s.Title = N'La vie en rose' AND t.French = N'joie')
    INSERT INTO SongTerm (SongsId, TermsId)
    SELECT s.Id, t.Id FROM Songs s CROSS JOIN Terms t
    WHERE s.Title=N'La vie en rose' AND t.French=N'joie';

    -- Non, je ne regrette rien: regret, liberté
    IF NOT EXISTS (SELECT 1 FROM SongTerm st
                   JOIN Songs s ON s.Id = st.SongsId
                   JOIN Terms t ON t.Id = st.TermsId
                   WHERE s.Title = N'Non, je ne regrette rien' AND t.French = N'regret')
    INSERT INTO SongTerm (SongsId, TermsId)
    SELECT s.Id, t.Id FROM Songs s CROSS JOIN Terms t
    WHERE s.Title=N'Non, je ne regrette rien' AND t.French=N'regret';

    IF NOT EXISTS (SELECT 1 FROM SongTerm st
                   JOIN Songs s ON s.Id = st.SongsId
                   JOIN Terms t ON t.Id = st.TermsId
                   WHERE s.Title = N'Non, je ne regrette rien' AND t.French = N'liberté')
    INSERT INTO SongTerm (SongsId, TermsId)
    SELECT s.Id, t.Id FROM Songs s CROSS JOIN Terms t
    WHERE s.Title=N'Non, je ne regrette rien' AND t.French=N'liberté';

    -- Ne me quitte pas: tristesse, amour
    IF NOT EXISTS (SELECT 1 FROM SongTerm st
                   JOIN Songs s ON s.Id = st.SongsId
                   JOIN Terms t ON t.Id = st.TermsId
                   WHERE s.Title = N'Ne me quitte pas' AND t.French = N'tristesse')
    INSERT INTO SongTerm (SongsId, TermsId)
    SELECT s.Id, t.Id FROM Songs s CROSS JOIN Terms t
    WHERE s.Title=N'Ne me quitte pas' AND t.French=N'tristesse';

    IF NOT EXISTS (SELECT 1 FROM SongTerm st
                   JOIN Songs s ON s.Id = st.SongsId
                   JOIN Terms t ON t.Id = st.TermsId
                   WHERE s.Title = N'Ne me quitte pas' AND t.French = N'amour')
    INSERT INTO SongTerm (SongsId, TermsId)
    SELECT s.Id, t.Id FROM Songs s CROSS JOIN Terms t
    WHERE s.Title=N'Ne me quitte pas' AND t.French=N'amour';

    -- Sous le ciel de Paris: ciel, joie
    IF NOT EXISTS (SELECT 1 FROM SongTerm st
                   JOIN Songs s ON s.Id = st.SongsId
                   JOIN Terms t ON t.Id = st.TermsId
                   WHERE s.Title = N'Sous le ciel de Paris' AND t.French = N'ciel')
    INSERT INTO SongTerm (SongsId, TermsId)
    SELECT s.Id, t.Id FROM Songs s CROSS JOIN Terms t
    WHERE s.Title=N'Sous le ciel de Paris' AND t.French=N'ciel';

    IF NOT EXISTS (SELECT 1 FROM SongTerm st
                   JOIN Songs s ON s.Id = st.SongsId
                   JOIN Terms t ON t.Id = st.TermsId
                   WHERE s.Title = N'Sous le ciel de Paris' AND t.French = N'joie')
    INSERT INTO SongTerm (SongsId, TermsId)
    SELECT s.Id, t.Id FROM Songs s CROSS JOIN Terms t
    WHERE s.Title=N'Sous le ciel de Paris' AND t.French=N'joie';

    -- La mer: mer, joie
    IF NOT EXISTS (SELECT 1 FROM SongTerm st
                   JOIN Songs s ON s.Id = st.SongsId
                   JOIN Terms t ON t.Id = st.TermsId
                   WHERE s.Title = N'La mer' AND t.French = N'mer')
    INSERT INTO SongTerm (SongsId, TermsId)
    SELECT s.Id, t.Id FROM Songs s CROSS JOIN Terms t
    WHERE s.Title=N'La mer' AND t.French=N'mer';

    IF NOT EXISTS (SELECT 1 FROM SongTerm st
                   JOIN Songs s ON s.Id = st.SongsId
                   JOIN Terms t ON t.Id = st.TermsId
                   WHERE s.Title = N'La mer' AND t.French = N'joie')
    INSERT INTO SongTerm (SongsId, TermsId)
    SELECT s.Id, t.Id FROM Songs s CROSS JOIN Terms t
    WHERE s.Title=N'La mer' AND t.French=N'joie';

    -- Les copains d’abord: ami, joie
    IF NOT EXISTS (SELECT 1 FROM SongTerm st
                   JOIN Songs s ON s.Id = st.SongsId
                   JOIN Terms t ON t.Id = st.TermsId
                   WHERE s.Title = N'Les copains d’abord' AND t.French = N'ami')
    INSERT INTO SongTerm (SongsId, TermsId)
    SELECT s.Id, t.Id FROM Songs s CROSS JOIN Terms t
    WHERE s.Title=N'Les copains d’abord' AND t.French=N'ami';

    IF NOT EXISTS (SELECT 1 FROM SongTerm st
                   JOIN Songs s ON s.Id = st.SongsId
                   JOIN Terms t ON t.Id = st.TermsId
                   WHERE s.Title = N'Les copains d’abord' AND t.French = N'joie')
    INSERT INTO SongTerm (SongsId, TermsId)
    SELECT s.Id, t.Id FROM Songs s CROSS JOIN Terms t
    WHERE s.Title=N'Les copains d’abord' AND t.French=N'joie';

    -- Paroles, paroles: paroles
    IF NOT EXISTS (SELECT 1 FROM SongTerm st
                   JOIN Songs s ON s.Id = st.SongsId
                   JOIN Terms t ON t.Id = st.TermsId
                   WHERE s.Title = N'Paroles, paroles' AND t.French = N'paroles')
    INSERT INTO SongTerm (SongsId, TermsId)
    SELECT s.Id, t.Id FROM Songs s CROSS JOIN Terms t
    WHERE s.Title=N'Paroles, paroles' AND t.French=N'paroles';
END

PRINT 'Seed completed (artists/songs/terms).';
