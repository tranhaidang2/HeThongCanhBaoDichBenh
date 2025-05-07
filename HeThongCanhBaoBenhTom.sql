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
    Email NVARCHAR(100),
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

INSERT INTO [Role] (RoleName, Description)
VALUES 
    (N'Admin', N'Tài khoản quản trị hệ thống'),
    (N'User', N'Tài khoản người dùng thông thường'),
    (N'Moderator', N'Quản lý nội dung hoặc bài viết');

Select * from [User]


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


SELECT * FROM [User]

INSERT INTO [News] (NewsTitle, NewsShortDescription, NewsContent, UserId)
VALUES (
    N'Phòng chống bệnh đốm trắng trên tôm',
    N'Các biện pháp phòng ngừa hiệu quả bệnh đốm trắng do virus WSSV gây ra.',
    N'Bệnh đốm trắng là một trong những bệnh nguy hiểm nhất đối với tôm nuôi. Để phòng bệnh hiệu quả, người nuôi cần chú trọng đến việc chọn giống sạch bệnh, quản lý môi trường nuôi, và thường xuyên theo dõi sức khỏe của tôm. Ngoài ra, việc bổ sung chế phẩm sinh học giúp tăng cường sức đề kháng cũng rất cần thiết.',
    '0ff4627c-81a2-4ccb-a6c9-cb7beb8ed65a'
);
INSERT INTO [News] (NewsTitle, NewsShortDescription, NewsContent, UserId)
VALUES (
    N'Tác hại của virus WSSV trong ao nuôi',
    N'Tìm hiểu ảnh hưởng nghiêm trọng của virus gây bệnh đốm trắng đối với tôm.',
    N'Virus WSSV (White Spot Syndrome Virus) có tốc độ lây lan rất nhanh trong điều kiện ao nuôi không đảm bảo vệ sinh. Chỉ trong vòng vài ngày, tôm có thể chết hàng loạt. Việc kiểm tra định kỳ nguồn nước và sát trùng ao nuôi là biện pháp quan trọng để hạn chế sự phát tán của loại virus này.',
    '0ff4627c-81a2-4ccb-a6c9-cb7beb8ed65a'
);

INSERT INTO [News] (NewsTitle, NewsShortDescription, NewsContent, UserId) VALUES
(N'Phòng tránh bệnh phân trắng ở tôm',
N'Nguyên nhân và cách phòng chống bệnh phân trắng thường gặp trong ao nuôi tôm.',
N'Bệnh phân trắng trên tôm thường do vi khuẩn đường ruột hoặc ký sinh trùng gây ra. Cần kiểm tra chất lượng thức ăn, tránh cho ăn dư thừa và sử dụng men vi sinh để cải thiện hệ tiêu hóa cho tôm.', 
'0ff4627c-81a2-4ccb-a6c9-cb7beb8ed65a'),

(N'Bệnh hoại tử gan tụy cấp tính (AHPND)',
N'AHPND là bệnh nguy hiểm, gây chết hàng loạt ở tôm sú và thẻ chân trắng.',
N'Nguyên nhân chính của bệnh là do vi khuẩn Vibrio parahaemolyticus mang plasmid độc tố. Biện pháp phòng chống bao gồm xử lý nước đầu vào, sử dụng giống sạch bệnh và hạn chế sử dụng kháng sinh bừa bãi.', 
'0ff4627c-81a2-4ccb-a6c9-cb7beb8ed65a'),

(N'Phòng trị bệnh đen mang ở tôm',
N'Tìm hiểu về nguyên nhân gây đen mang và cách điều trị hiệu quả.',
N'Đen mang có thể do chất lượng nước kém, tích tụ kim loại nặng hoặc do nấm. Người nuôi nên định kỳ thay nước, sử dụng khoáng chất hợp lý và cải thiện môi trường đáy ao.', 
'0ff4627c-81a2-4ccb-a6c9-cb7beb8ed65a'),

(N'Bệnh mòn đuôi và mòn chân tôm',
N'Biểu hiện, nguyên nhân và cách phòng trị bệnh mòn đuôi ở tôm nuôi.',
N'Nguyên nhân chủ yếu do vi khuẩn và điều kiện nuôi kém. Cần duy trì chất lượng nước ổn định, bổ sung vitamin C và men tiêu hóa để tăng đề kháng cho tôm.', 
'0ff4627c-81a2-4ccb-a6c9-cb7beb8ed65a'),

(N'Bệnh cong thân và đục cơ trên tôm',
N'Cách nhận biết và phòng tránh bệnh cong thân thường gặp trong mùa lạnh.',
N'Bệnh do virus hoặc do sốc môi trường (thay đổi nhiệt độ, độ mặn đột ngột). Cần quản lý nhiệt độ ao nuôi hợp lý, tránh stress cho tôm và dùng chế phẩm sinh học hỗ trợ.', 
'0ff4627c-81a2-4ccb-a6c9-cb7beb8ed65a'),

(N'Bệnh nấm trên tôm nuôi',
N'Nguyên nhân gây bệnh nấm và cách phòng tránh hiệu quả.',
N'Nấm thường phát triển khi môi trường nuôi ô nhiễm, tôm bị trầy xước. Người nuôi cần vệ sinh ao nuôi sạch sẽ, tránh mật độ quá cao và có thể sử dụng thuốc tím để xử lý nấm ở giai đoạn đầu.', 
'0ff4627c-81a2-4ccb-a6c9-cb7beb8ed65a'),

(N'Bệnh đường ruột trên tôm',
N'Nguyên nhân phổ biến và giải pháp phòng bệnh đường ruột ở tôm.',
N'Tôm bị bệnh đường ruột sẽ chậm lớn, phân lỏng hoặc đứt khúc. Cần chú trọng đến khẩu phần ăn, dùng men vi sinh định kỳ và kiểm tra lại nguồn nước thường xuyên.', 
'0ff4627c-81a2-4ccb-a6c9-cb7beb8ed65a'),

(N'Bệnh vi bào tử trùng (EHP) trên tôm',
N'EHP gây chậm lớn và giảm hiệu quả nuôi tôm thẻ chân trắng.',
N'EHP là ký sinh trùng ký sinh trong tế bào gan tụy. Tôm nhiễm bệnh sẽ ăn ít và phát triển chậm. Nên sử dụng giống sạch bệnh, quản lý thức ăn và chất lượng nước tốt để hạn chế mầm bệnh.', 
'0ff4627c-81a2-4ccb-a6c9-cb7beb8ed65a'),

(N'Phòng bệnh đốm đen trên vỏ tôm',
N'Đốm đen làm giảm giá trị thương phẩm của tôm nuôi.',
N'Đốm đen có thể do vi khuẩn, nấm hoặc ô nhiễm môi trường. Cần giữ môi trường ao sạch, bổ sung khoáng và vitamin để nâng cao chất lượng vỏ tôm.', 
'0ff4627c-81a2-4ccb-a6c9-cb7beb8ed65a'),

(N'Bệnh đỏ thân ở tôm sú',
N'Nguyên nhân, biểu hiện và cách phòng ngừa bệnh đỏ thân.',
N'Bệnh đỏ thân thường do sốc môi trường hoặc vi khuẩn. Tôm sẽ có màu đỏ bất thường, yếu và chết nhanh. Cần điều chỉnh môi trường nước, tăng sức đề kháng bằng chế phẩm sinh học.', 
'0ff4627c-81a2-4ccb-a6c9-cb7beb8ed65a');

INSERT INTO [Image] (NewsID, ImagePath)
VALUES 
(1, N'images/news/white_spot_prevention.jpg');

INSERT INTO [Image] (NewsID, ImagePath)
VALUES 
(2, N'images/news/wssv_impact.jpg');

-- Chèn dữ liệu vào bảng Disease
INSERT INTO [Disease] (DiseaseName, Description, Symptoms, Cause)
VALUES 
(N'Bệnh Đốm Trắng', N'Bệnh đốm trắng là một bệnh nhiễm virus ảnh hưởng đến tôm, gây tổn thương cơ thể.', N'Đốm trắng trên cơ thể tôm, chết đột ngột', N'Virus White Spot Syndrome Virus (WSSV)'),
(N'Bệnh Vàng Vây', N'Bệnh vàng vây gây suy yếu hệ thống miễn dịch và làm chết tôm', N'Màu sắc tôm thay đổi, vây bị tổn thương', N'Virus Yellow Head Virus (YHV)'),
(N'Bệnh Đốm Đen', N'Bệnh đốm đen gây tổn thương nghiêm trọng cho tôm, giảm khả năng sinh sản.', N'Đốm đen xuất hiện trên cơ thể tôm, tôm bị suy yếu', N'Vi khuẩn Vibrio spp.'),
(N'Bệnh Bọt Nước', N'Bệnh bọt nước gây rối loạn sinh lý, ảnh hưởng đến sự phát triển của tôm.', N'Bọt nước xuất hiện ở phần bụng, tôm bỏ ăn', N'Nấm, vi khuẩn, môi trường nuôi không tốt'),
(N'Bệnh Nấm Mốc', N'Bệnh nấm mốc ảnh hưởng đến tôm trong các giai đoạn phát triển ban đầu.', N'Màu trắng như bột phủ trên cơ thể tôm, tôm không ăn', N'Nấm Fusarium và Aspergillus');
GO

-- Chèn dữ liệu vào bảng Prevention
INSERT INTO [Prevention] (Method, DiseaseId)
VALUES 
(N'Sử dụng thuốc kháng virus để điều trị tôm bị bệnh đốm trắng.', 1), -- DiseaseId = 1
(N'Điều chỉnh môi trường nước, sử dụng thuốc kháng sinh để phòng ngừa bệnh vàng vây.', 2), -- DiseaseId = 2
(N'Duy trì môi trường nuôi sạch sẽ, kiểm tra chất lượng nước để ngăn ngừa bệnh đốm đen.', 3), -- DiseaseId = 3
(N'Cải thiện chất lượng nước và giảm sự ô nhiễm để ngăn ngừa bệnh bọt nước.', 4), -- DiseaseId = 4
(N'Sử dụng thuốc diệt nấm và kháng sinh, đảm bảo môi trường nuôi luôn sạch sẽ để phòng ngừa bệnh nấm mốc.', 5); -- DiseaseId = 5
GO


SELECT * FROM [Disease]