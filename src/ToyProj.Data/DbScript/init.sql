-- Create tables with auto-increment IDs and conditionally if not exists for SQL Server

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Country]') AND type in (N'U'))
BEGIN
    CREATE TABLE Country (
        CountryId INT IDENTITY(1,1) PRIMARY KEY,
        CountryIsoCode VARCHAR(10),
        CountryName VARCHAR(100)
    );
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductionCompany]') AND type in (N'U'))
BEGIN
    CREATE TABLE ProductionCompany (
        CompanyId INT IDENTITY(1,1) PRIMARY KEY,
        CompanyName VARCHAR(100)
    );
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Language]') AND type in (N'U'))
BEGIN
    CREATE TABLE Language (
        LanguageId INT IDENTITY(1,1) PRIMARY KEY,
        LanguageCode VARCHAR(10),
        LanguageName VARCHAR(100)
    );
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LanguageRole]') AND type in (N'U'))
BEGIN
    CREATE TABLE LanguageRole (
        RoleId INT IDENTITY(1,1) PRIMARY KEY,
        LanguageRole VARCHAR(100)
    );
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Genre]') AND type in (N'U'))
BEGIN
    CREATE TABLE Genre (
        GenreId INT IDENTITY(1,1) PRIMARY KEY,
        GenreName VARCHAR(100)
    );
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Keyword]') AND type in (N'U'))
BEGIN
    CREATE TABLE Keyword (
        KeywordId INT IDENTITY(1,1) PRIMARY KEY,
        KeywordName VARCHAR(100)
    );
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Person]') AND type in (N'U'))
BEGIN
    CREATE TABLE Person (
        PersonId INT IDENTITY(1,1) PRIMARY KEY,
        PersonName VARCHAR(100)
    );
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Gender]') AND type in (N'U'))
BEGIN
    CREATE TABLE Gender (
        GenderId INT IDENTITY(1,1) PRIMARY KEY,
        Gender VARCHAR(10)
    );
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Department]') AND type in (N'U'))
BEGIN
    CREATE TABLE Department (
        DepartmentId INT IDENTITY(1,1) PRIMARY KEY,
        DepartmentName VARCHAR(100)
    );
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Movie]') AND type in (N'U'))
BEGIN
    CREATE TABLE Movie (
        MovieId INT IDENTITY(1,1) PRIMARY KEY,
        Title VARCHAR(100),
        Budget INT,
        Homepage VARCHAR(255),
        Overview TEXT,
        Popularity DECIMAL(10, 2),
        ReleaseDate DATE,
        Revenue INT,
        Runtime INT,
        MovieStatus VARCHAR(50),
        Tagline VARCHAR(255),
        VotesAvg DECIMAL(3, 1),
        VotesCount INT,
        Thumbnail VARCHAR(255)
    );
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductionCountry]') AND type in (N'U'))
BEGIN
    CREATE TABLE ProductionCountry (
        MovieId INT,
        CountryId INT,
        PRIMARY KEY (MovieId, CountryId),
        FOREIGN KEY (MovieId) REFERENCES Movie(MovieId),
        FOREIGN KEY (CountryId) REFERENCES Country(CountryId)
    );
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MovieCompany]') AND type in (N'U'))
BEGIN
    CREATE TABLE MovieCompany (
        MovieId INT,
        CompanyId INT,
        PRIMARY KEY (MovieId, CompanyId),
        FOREIGN KEY (MovieId) REFERENCES Movie(MovieId),
        FOREIGN KEY (CompanyId) REFERENCES ProductionCompany(CompanyId)
    );
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MovieLanguages]') AND type in (N'U'))
BEGIN
    CREATE TABLE MovieLanguages (
        MovieId INT,
        LanguageId INT,
        LanguageRoleId INT,
        PRIMARY KEY (MovieId, LanguageId, LanguageRoleId),
        FOREIGN KEY (MovieId) REFERENCES Movie(MovieId),
        FOREIGN KEY (LanguageId) REFERENCES Language(LanguageId),
        FOREIGN KEY (LanguageRoleId) REFERENCES LanguageRole(RoleId)
    );
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MovieGenre]') AND type in (N'U'))
BEGIN
    CREATE TABLE MovieGenre (
        MovieId INT,
        GenreId INT,
        PRIMARY KEY (MovieId, GenreId),
        FOREIGN KEY (MovieId) REFERENCES Movie(MovieId),
        FOREIGN KEY (GenreId) REFERENCES Genre(GenreId)
    );
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MovieKeywords]') AND type in (N'U'))
BEGIN
    CREATE TABLE MovieKeywords (
        MovieId INT,
        KeywordId INT,
        PRIMARY KEY (MovieId, KeywordId),
        FOREIGN KEY (MovieId) REFERENCES Movie(MovieId),
        FOREIGN KEY (KeywordId) REFERENCES Keyword(KeywordId)
    );
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MovieCast]') AND type in (N'U'))
BEGIN
    CREATE TABLE MovieCast (
        MovieId INT,
        PersonId INT,
        GenderId INT,
        CharacterName VARCHAR(100),
        CastOrder INT,
        PRIMARY KEY (MovieId, PersonId),
        FOREIGN KEY (MovieId) REFERENCES Movie(MovieId),
        FOREIGN KEY (PersonId) REFERENCES Person(PersonId),
        FOREIGN KEY (GenderId) REFERENCES Gender(GenderId)
    );
END

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MovieCrew]') AND type in (N'U'))
BEGIN
    CREATE TABLE MovieCrew (
        MovieId INT,
        PersonId INT,
        DepartmentId INT,
        Job VARCHAR(100),
        PRIMARY KEY (MovieId, PersonId, DepartmentId),
        FOREIGN KEY (MovieId) REFERENCES Movie(MovieId),
        FOREIGN KEY (PersonId) REFERENCES Person(PersonId),
        FOREIGN KEY (DepartmentId) REFERENCES Department(DepartmentId)
    );
END



-- Sample data for Country table
INSERT INTO Country (CountryIsoCode, CountryName) VALUES
('USA', 'United States'),
('CAN', 'Canada'),
('GBR', 'United Kingdom'),
('FRA', 'France'),
('DEU', 'Germany'),
('JPN', 'Japan'),
('AUS', 'Australia'),
('BRA', 'Brazil'),
('IND', 'India'),
('RUS', 'Russia');

-- Sample data for ProductionCompany table
INSERT INTO ProductionCompany (CompanyName) VALUES
('Warner Bros.'),
('Universal Pictures'),
('Paramount Pictures'),
('Walt Disney Pictures'),
('20th Century Fox'),
('Columbia Pictures'),
('Lionsgate Films'),
('MGM Studios'),
('DreamWorks'),
('Pixar');

-- Sample data for Language table
INSERT INTO Language (LanguageCode, LanguageName) VALUES
('EN', 'English'),
('FR', 'French'),
('DE', 'German'),
('ES', 'Spanish'),
('IT', 'Italian'),
('PT', 'Portuguese'),
('ZH', 'Chinese'),
('JA', 'Japanese'),
('RU', 'Russian'),
('HI', 'Hindi');

-- Sample data for LanguageRole table
INSERT INTO LanguageRole (LanguageRole) VALUES
('Original'),
('Dubbed'),
('Subtitled'),
('Narration'),
('Voice Over'),
('Interpreter'),
('Sign Language'),
('Commentary'),
('Live Translation'),
('Multilingual');

-- Sample data for Genre table
INSERT INTO Genre (GenreName) VALUES
('Action'),
('Adventure'),
('Animation'),
('Biography'),
('Comedy'),
('Crime'),
('Documentary'),
('Drama'),
('Fantasy'),
('Horror');

-- Sample data for Keyword table
INSERT INTO Keyword (KeywordName) VALUES
('love'),
('friendship'),
('betrayal'),
('revenge'),
('hero'),
('villain'),
('journey'),
('mystery'),
('magic'),
('battle');

-- Sample data for Person table
INSERT INTO Person (PersonName) VALUES
('John Smith'),
('Jane Doe'),
('Michael Johnson'),
('Emily Davis'),
('James Brown'),
('Chris Evans'),
('Scarlett Johansson'),
('Robert Downey Jr.'),
('Natalie Portman'),
('Leonardo DiCaprio');

-- Sample data for Gender table
INSERT INTO Gender (Gender) VALUES
('Male'),
('Female')

-- Sample data for Department table
INSERT INTO Department (DepartmentName) VALUES
('Directing'),
('Writing'),
('Cinematography'),
('Editing'),
('Production Design'),
('Sound Design'),
('Costume Design'),
('Visual Effects'),
('Makeup'),
('Lighting');

-- Sample data for Movie table (20 records)
INSERT INTO Movie (Title, Budget, Homepage, Overview, Popularity, ReleaseDate, Revenue, Runtime, MovieStatus, Tagline, VotesAvg, VotesCount, Thumbnail) VALUES
('The Great Adventure', 100000000, 'http://greatadventure.com', 'An epic journey of discovery.', 85.5, '2023-05-25', 500000000, 120, 'Released', 'Adventure awaits.', 8.2, 1000, 'http://image1.com'),
('Space Odyssey', 150000000, 'http://spaceodyssey.com', 'A thrilling journey through space.', 90.2, '2022-11-18', 750000000, 150, 'Released', 'To infinity and beyond.', 9.0, 2000, 'http://image2.com'),
('Love Story', 20000000, 'http://lovestory.com', 'A timeless tale of romance.', 70.4, '2021-02-14', 100000000, 95, 'Released', 'Love conquers all.', 7.5, 500, 'http://image3.com'),
('Haunted House', 30000000, 'http://hauntedhouse.com', 'A chilling horror story.', 65.8, '2023-10-31', 150000000, 105, 'Released', 'Fear the unknown.', 6.8, 800, 'http://image4.com'),
('Comedy Night', 25000000, 'http://comedynight.com', 'A night of laughter and fun.', 75.1, '2023-07-15', 120000000, 110, 'Released', 'Laugh out loud.', 7.8, 600, 'http://image5.com'),
('Mystery Manor', 45000000, 'http://mysterymanor.com', 'A thrilling mystery unfolds.', 60.0, '2023-08-20', 180000000, 130, 'Released', 'Unveil the secrets.', 7.0, 700, 'http://image6.com'),
('Fantasy World', 80000000, 'http://fantasyworld.com', 'A magical journey.', 88.8, '2023-12-25', 600000000, 140, 'Released', 'Believe in magic.', 8.5, 1500, 'http://image7.com'),
('Heroic Tales', 95000000, 'http://heroictales.com', 'Stories of heroes and legends.', 77.7, '2024-01-15', 400000000, 125, 'Released', 'Be a hero.', 7.9, 1100, 'http://image8.com'),
('Romantic Getaway', 30000000, 'http://romanticgetaway.com', 'A romantic escape.', 68.9, '2023-09-14', 150000000, 100, 'Released', 'Love is in the air.', 7.4, 900, 'http://image9.com'),
('Sci-Fi Saga', 120000000, 'http://scifisaga.com', 'An intergalactic adventure.', 92.5, '2023-11-11', 900000000, 160, 'Released', 'Beyond the stars.', 9.3, 2500, 'http://image10.com'),
('Epic Fantasy', 110000000, 'http://epicfantasy.com', 'A world of magic and dragons.', 85.0, '2023-10-05', 850000000, 140, 'Released', 'Magic is real.', 8.0, 2000, 'http://image11.com'),
('Historical Drama', 50000000, 'http://historicaldrama.com', 'A tale from the past.', 70.5, '2022-09-21', 200000000, 120, 'Released', 'History comes alive.', 7.6, 1200, 'http://image12.com'),
('Animated Adventure', 60000000, 'http://animatedadventure.com', 'An animated journey.', 75.9, '2023-08-10', 300000000, 110, 'Released', 'Animation like never before.', 7.9, 1300, 'http://image13.com'),
('Thriller Night', 40000000, 'http://thrillernight.com', 'A night of suspense.', 68.3, '2023-05-01', 250000000, 100, 'Released', 'Keep the lights on.', 7.2, 1100, 'http://image14.com'),
('Science Wonder', 70000000, 'http://sciencewonder.com', 'Exploring scientific wonders.', 80.2, '2023-11-20', 500000000, 130, 'Released', 'Science meets imagination.', 8.3, 1600, 'http://image15.com'),
('Dark Mystery', 35000000, 'http://darkmystery.com', 'A dark tale of mystery.', 65.0, '2023-07-07', 180000000, 105, 'Released', 'Can you solve it?', 6.9, 800, 'http://image16.com'),
('Romantic Comedy', 28000000, 'http://romanticcomedy.com', 'A funny tale of love.', 72.1, '2023-02-14', 150000000, 115, 'Released', 'Laugh and love.', 7.7, 900, 'http://image17.com'),
('Action Heroes', 140000000, 'http://actionheroes.com', 'Action-packed heroics.', 88.4, '2023-04-05', 900000000, 150, 'Released', 'Heroes save the day.', 8.6, 2200, 'http://image18.com'),
('Magic Realm', 100000000, 'http://magicrealm.com', 'A journey into the magical realm.', 82.7, '2023-03-30', 700000000, 130, 'Released', 'Magic is all around.', 8.1, 1900, 'http://image19.com'),
('Spy Thriller', 90000000, 'http://spythriller.com', 'A tale of spies and intrigue.', 78.6, '2023-06-18', 600000000, 120, 'Released', 'Trust no one.', 7.8, 1700, 'http://image20.com');

-- Sample data for ProductionCountry table
INSERT INTO ProductionCountry (MovieId, CountryId) VALUES
(1, 1),
(2, 1),
(3, 2),
(4, 3),
(5, 4),
(6, 5),
(7, 6),
(8, 7),
(9, 8),
(10, 9),
(11, 10),
(12, 1),
(13, 2),
(14, 3),
(15, 4),
(16, 5),
(17, 6),
(18, 7),
(19, 8),
(20, 9);

-- Sample data for MovieCompany table
INSERT INTO MovieCompany (MovieId, CompanyId) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10),
(11, 1),
(12, 2),
(13, 3),
(14, 4),
(15, 5),
(16, 6),
(17, 7),
(18, 8),
(19, 9),
(20, 10);

-- Sample data for MovieLanguages table
INSERT INTO MovieLanguages (MovieId, LanguageId, LanguageRoleId) VALUES
(1, 1, 1),
(2, 1, 1),
(3, 2, 2),
(4, 3, 3),
(5, 4, 4),
(6, 5, 5),
(7, 6, 6),
(8, 7, 7),
(9, 8, 8),
(10, 9, 9),
(11, 10, 10),
(12, 1, 1),
(13, 2, 2),
(14, 3, 3),
(15, 4, 4),
(16, 5, 5),
(17, 6, 6),
(18, 7, 7),
(19, 8, 8),
(20, 9, 9);

-- Sample data for MovieGenre table
INSERT INTO MovieGenre (MovieId, GenreId) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10),
(11, 1),
(12, 2),
(13, 3),
(14, 4),
(15, 5),
(16, 6),
(17, 7),
(18, 8),
(19, 9),
(20, 10);

-- Sample data for MovieKeywords table
INSERT INTO MovieKeywords (MovieId, KeywordId) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10),
(11, 1),
(12, 2),
(13, 3),
(14, 4),
(15, 5),
(16, 6),
(17, 7),
(18, 8),
(19, 9),
(20, 10);

-- Sample data for MovieCast table
INSERT INTO MovieCast (MovieId, PersonId, GenderId, CharacterName, CastOrder) VALUES
(1, 1, 1, 'John Doe', 1),
(2, 2, 2, 'Jane Smith', 2),
(3, 3, 2, 'Michael Brown', 3),
(4, 4, 1, 'Emily White', 4),
(5, 5, 2, 'James Green', 5),
(6, 6, 1, 'Chris Black', 6),
(7, 7, 2, 'Scarlett Red', 7),
(8, 8, 1, 'Robert Blue', 8),
(9, 9, 2, 'Natalie Purple', 9),
(10, 10, 1, 'Leo Yellow', 10),
(11, 1, 2, 'John Doe', 1),
(12, 2, 1, 'Jane Smith', 2),
(13, 3, 2, 'Michael Brown', 3),
(14, 4, 2, 'Emily White', 4),
(15, 5, 1, 'James Green', 5),
(16, 6, 1, 'Chris Black', 6),
(17, 7, 2, 'Scarlett Red', 7),
(18, 8, 2, 'Robert Blue', 8),
(19, 9, 1, 'Natalie Purple', 9),
(20, 10, 1, 'Leo Yellow', 10);

-- Sample data for MovieCrew table
INSERT INTO MovieCrew (MovieId, PersonId, DepartmentId, Job) VALUES
(1, 1, 1, 'Director'),
(2, 2, 2, 'Writer'),
(3, 3, 3, 'Cinematographer'),
(4, 4, 4, 'Editor'),
(5, 5, 5, 'Production Designer'),
(6, 6, 6, 'Sound Designer'),
(7, 7, 7, 'Costume Designer'),
(8, 8, 8, 'Visual Effects Artist'),
(9, 9, 9, 'Makeup Artist'),
(10, 10, 10, 'Lighting Technician'),
(11, 1, 1, 'Director'),
(12, 2, 2, 'Writer'),
(13, 3, 3, 'Cinematographer'),
(14, 4, 4, 'Editor'),
(15, 5, 5, 'Production Designer'),
(16, 6, 6, 'Sound Designer'),
(17, 7, 7, 'Costume Designer'),
(18, 8, 8, 'Visual Effects Artist'),
(19, 9, 9, 'Makeup Artist'),
(20, 10, 10, 'Lighting Technician');
