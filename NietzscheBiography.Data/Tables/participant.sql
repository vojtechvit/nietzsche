CREATE TABLE [nietzschebiography].[participant] (
    [id]          BIGINT         IDENTITY (877, 1) NOT NULL,
    [name]        NVARCHAR (100) NOT NULL,
    [description] NVARCHAR (MAX) NULL,
    [type]        TINYINT  NOT NULL,
    CONSTRAINT [PK_participant_id] PRIMARY KEY CLUSTERED ([id] ASC)
);

