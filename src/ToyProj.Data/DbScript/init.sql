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
('DEU', 'Germany');

-- Sample data for ProductionCompany table
INSERT INTO ProductionCompany (CompanyName) VALUES
('Warner Bros.'),
('Universal Pictures'),
('Paramount Pictures'),
('Walt Disney Pictures'),
('20th Century Fox');

-- Sample data for Language table
INSERT INTO Language (LanguageCode, LanguageName) VALUES
('EN', 'English'),
('FR', 'French'),
('DE', 'German'),
('ES', 'Spanish'),
('IT', 'Italian');

-- Sample data for LanguageRole table
INSERT INTO LanguageRole (LanguageRole) VALUES
('Original'),
('Dubbed'),
('Subtitled'),
('Narration'),
('Voice Over');

-- Sample data for Genre table
INSERT INTO Genre (GenreName) VALUES
('Action'),
('Adventure'),
('Animation'),
('Biography'),
('Comedy');

-- Sample data for Keyword table
INSERT INTO Keyword (KeywordName) VALUES
('love'),
('friendship'),
('betrayal'),
('revenge'),
('hero');

-- Sample data for Person table
INSERT INTO Person (PersonName) VALUES
('John Smith'),
('Jane Doe'),
('Michael Johnson'),
('Emily Davis'),
('James Brown');

-- Sample data for Gender table
INSERT INTO Gender (Gender) VALUES
('Male'),
('Female');

-- Sample data for Department table
INSERT INTO Department (DepartmentName) VALUES
('Directing'),
('Writing'),
('Cinematography'),
('Editing'),
('Production Design');

-- Sample data for Movie table
INSERT INTO Movie (Title, Budget, Homepage, Overview, Popularity, ReleaseDate, Revenue, Runtime, MovieStatus, Tagline, VotesAvg, VotesCount, Thumbnail) VALUES
('The Great Adventure', 100000000, 'http://greatadventure.com', 'An epic journey of discovery.', 85.5, '2023-05-25', 500000000, 120, 'Released', 'Adventure awaits.', 8.2, 1000, 'http://image1.com'),
('Space Odyssey', 150000000, 'http://spaceodyssey.com', 'A thrilling journey through space.', 90.2, '2022-11-18', 750000000, 150, 'Released', 'To infinity and beyond.', 9.0, 2000, 'http://image2.com'),
('Love Story', 20000000, 'http://lovestory.com', 'A timeless tale of romance.', 70.4, '2021-02-14', 100000000, 95, 'Released', 'Love conquers all.', 7.5, 500, 'http://image3.com'),
('Haunted House', 30000000, 'http://hauntedhouse.com', 'A chilling horror story.', 65.8, '2023-10-31', 150000000, 105, 'Released', 'Fear the unknown.', 6.8, 800, 'http://image4.com'),
('Comedy Night', 25000000, 'http://comedynight.com', 'A night of laughter and fun.', 75.1, '2023-07-15', 120000000, 110, 'Released', 'Laugh out loud.', 7.8, 600, 'http://image5.com');

-- Sample data for ProductionCountry table
INSERT INTO ProductionCountry (MovieId, CountryId) VALUES
(1, 1),
(2, 1),
(3, 2),
(4, 3),
(5, 4);

-- Sample data for MovieCompany table
INSERT INTO MovieCompany (MovieId, CompanyId) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5);

-- Sample data for MovieLanguages table
INSERT INTO MovieLanguages (MovieId, LanguageId, LanguageRoleId) VALUES
(1, 1, 1),
(2, 1, 2),
(3, 2, 3),
(4, 3, 4),
(5, 4, 5);

-- Sample data for MovieGenre table
INSERT INTO MovieGenre (MovieId, GenreId) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5);

-- Sample data for MovieKeywords table
INSERT INTO MovieKeywords (MovieId, KeywordId) VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5);

-- Sample data for MovieCast table
INSERT INTO MovieCast (MovieId, PersonId, GenderId, CharacterName, CastOrder) VALUES
(1, 1, 1, 'Hero', 1),
(2, 2, 1, 'Heroine', 2),
(3, 3, 2, 'Villain', 3),
(4, 4, 2, 'Supporting Actor', 4),
(5, 5, 1, 'Supporting Actress', 5);

-- Sample data for MovieCrew table
INSERT INTO MovieCrew (MovieId, PersonId, DepartmentId, Job) VALUES
(1, 1, 1, 'Director'),
(2, 2, 2, 'Writer'),
(3, 3, 3, 'Cinematographer'),
(4, 4, 4, 'Editor'),
(5, 5, 5, 'Production Designer');