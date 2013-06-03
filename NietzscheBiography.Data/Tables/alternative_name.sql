CREATE TABLE [nietzschebiography].[alternative_name] (
    [id]             BIGINT         IDENTITY (7, 1) NOT NULL,
    [participant_id] BIGINT         NOT NULL,
    [name]           NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_alternative_name_id] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [alternative_name$fk_alternative_name_participant] FOREIGN KEY ([participant_id]) REFERENCES [nietzschebiography].[participant] ([id])
);


GO
CREATE NONCLUSTERED INDEX [fk_alternative_name_participant]
    ON [nietzschebiography].[alternative_name]([participant_id] ASC);

