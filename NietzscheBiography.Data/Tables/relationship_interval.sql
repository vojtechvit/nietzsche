CREATE TABLE [nietzschebiography].[relationship_interval] (
    [relationship_id] BIGINT NOT NULL,
    [interval_id]     BIGINT NOT NULL,
    CONSTRAINT [PK_relationship_interval_relationship_id] PRIMARY KEY CLUSTERED ([relationship_id] ASC, [interval_id] ASC),
    CONSTRAINT [relationship_interval$fk_relationship_interval_1] FOREIGN KEY ([relationship_id]) REFERENCES [nietzschebiography].[relationship] ([id]),
    CONSTRAINT [relationship_interval$fk_relationship_interval_2] FOREIGN KEY ([interval_id]) REFERENCES [nietzschebiography].[interval] ([id])
);


GO
CREATE NONCLUSTERED INDEX [fk_relationship_interval_2]
    ON [nietzschebiography].[relationship_interval]([interval_id] ASC);

