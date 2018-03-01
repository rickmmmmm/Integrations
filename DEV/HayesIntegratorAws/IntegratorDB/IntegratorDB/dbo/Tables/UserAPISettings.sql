CREATE TABLE [dbo].[UserAPISettings] (
    [Client]         VARCHAR (50)  NOT NULL,
    [UserName]       VARCHAR (50)  NOT NULL,
    [Passphrase]     VARCHAR (MAX) NULL,
    [Email]          VARCHAR (200) NULL,
    [Valid]          BIT           DEFAULT ((1)) NULL,
    [AddedDate]      DATETIME      DEFAULT (getdate()) NULL,
    [CertificateVal] VARCHAR (MAX) NULL,
    [ClientFullName] VARCHAR (200) NULL,
    [Support]        BIT           NULL,
    [Admin]          BIT           NULL,
    PRIMARY KEY CLUSTERED ([Client] ASC, [UserName] ASC)
);

