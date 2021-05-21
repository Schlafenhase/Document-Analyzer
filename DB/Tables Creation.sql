CREATE TABLE Employees (
    Id INT IDENTITY (1, 1) NOT NULL,
    Name VARCHAR(30) NOT NULL,
    PRIMARY KEY (Id),
    UNIQUE (Name)
);

CREATE TABLE Files (
    Id INT IDENTITY (1, 1) NOT NULL,
    Name VARCHAR(30) NOT NULL,
    Container VARCHAR(30) NOT NULL,
    NameAnalysis BIT NOT NULL,
    SentimentAnalysis BIT NOT NULL,
    SwearAnalysis BIT NOT NULL,
    PRIMARY KEY (Id)
);