CREATE TABLE [dbo].[ASNVerify](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CodASN] [nvarchar](100) NULL,
	[Details] [nvarchar](1000) NULL,
	[Available] [bit] NULL,
 CONSTRAINT [PK_ASNVerify] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


