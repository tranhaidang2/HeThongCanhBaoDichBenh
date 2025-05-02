USE HeThongCanhBaoBenhTom
GO

CREATE TABLE [Role] (
    RoleId INT PRIMARY KEY IDENTITY(1, 1),
    RoleName NVARCHAR(20) NOT NULL UNIQUE,
    Description NVARCHAR(100)
);
GO


CREATE TABLE [User] (
    UserId VARCHAR(36) PRIMARY KEY,
    Username NVARCHAR(30) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(512) NOT NULL,
    Email NVARCHAR(100) UNIQUE,
    Fullname NVARCHAR(50),
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    RoleId INT NOT NULL,
    CONSTRAINT FK_User_Role FOREIGN KEY (RoleId) REFERENCES [Role](RoleId)
);
GO

CREATE TABLE [Disease] (
    DiseaseId INT PRIMARY KEY IDENTITY(1, 1),
    DiseaseName NVARCHAR(100),
    Description NVARCHAR(MAX),
    Symptoms NVARCHAR(MAX),
    Cause NVARCHAR(MAX)
);
GO

CREATE TABLE [Prevention] (
    PreventionId INT PRIMARY KEY IDENTITY(1, 1),
    Method NVARCHAR(MAX),
    DiseaseId INT,
    CONSTRAINT FK_Prevention_Disease FOREIGN KEY (DiseaseId) REFERENCES [Disease](DiseaseId)
);
GO

CREATE TABLE [News] (
    NewsId INT PRIMARY KEY IDENTITY(1, 1),
    NewsTitle NVARCHAR(255),
    NewsShortDescription NVARCHAR(300) NULL,
    NewsContent NVARCHAR(MAX),
    NewsCreateAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    UserId VARCHAR(36),
    CONSTRAINT FK_News_User FOREIGN KEY (UserId) REFERENCES [User](UserId)
);
GO

CREATE TABLE [Post] (
    PostId INT PRIMARY KEY IDENTITY(1, 1),
    PostTitle NVARCHAR(255),
    PostContent NVARCHAR(MAX),
    PostCreateAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    UserId VARCHAR(36),
    CONSTRAINT FK_Post_User FOREIGN KEY (UserId) REFERENCES [User](UserId)
);


CREATE TABLE [Comment] (
    CommentId INT PRIMARY KEY IDENTITY(1,1),
    CommentContent NVARCHAR(MAX),
    CommentCreateAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    PostId INT, 
    UserId VARCHAR(36),
    CONSTRAINT FK_Comment_Post FOREIGN KEY (PostId) REFERENCES [Post](PostId),
    CONSTRAINT FK_Comment_User FOREIGN KEY (UserId) REFERENCES [User](UserId)
);
GO

-- Bảng Image: Quản lý ảnh
CREATE TABLE [Image] (
    ImageID INT PRIMARY KEY IDENTITY(1,1),
    NewsID INT NULL,
    DiseaseID INT NULL,
    PostID INT NULL,
    ImagePath NVARCHAR(255) NOT NULL,
    ImageCreateAt DATETIME2(3) DEFAULT GETDATE(),
    CONSTRAINT FK_Image_News FOREIGN KEY (NewsID) REFERENCES [News](NewsID),
    CONSTRAINT FK_Image_Disease FOREIGN KEY (DiseaseID) REFERENCES [Disease](DiseaseID),
    CONSTRAINT FK_Image_Post FOREIGN KEY (PostID) REFERENCES [Post](PostID)
);

-- Bỏ toàn bộ CONSTRAINT trước
ALTER TABLE [Comment] DROP CONSTRAINT FK_Comment_Post;
ALTER TABLE [Comment] DROP CONSTRAINT FK_Comment_User;
ALTER TABLE [Post] DROP CONSTRAINT FK_Post_User;
ALTER TABLE [News] DROP CONSTRAINT FK_News_User;
ALTER TABLE [Prevention] DROP CONSTRAINT FK_Prevention_Disease;
ALTER TABLE [User] DROP CONSTRAINT FK_User_Role;
ALTER TABLE [Image] DROP CONSTRAINT FK_Image_News;
ALTER TABLE [Image] DROP CONSTRAINT FK_Image_Disease;
ALTER TABLE [Image] DROP CONSTRAINT FK_Image_Post;
GO

-- Sau đó mới Drop từng bảng
DROP TABLE [Comment];
DROP TABLE [Post];
DROP TABLE [News];
DROP TABLE [Prevention];
DROP TABLE [Disease];
DROP TABLE [Image];
DROP TABLE [User];
DROP TABLE [Role];
GO
