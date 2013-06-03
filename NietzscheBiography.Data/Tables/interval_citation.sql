CREATE TABLE [nietzschebiography].[interval_citation] (
    [interval_id] BIGINT NOT NULL,
    [citation_id] BIGINT NOT NULL,
    CONSTRAINT [PK_interval_citation_interval_id] PRIMARY KEY CLUSTERED ([interval_id] ASC, [citation_id] ASC),
    CONSTRAINT [interval_citation$fk_interval_citation_1] FOREIGN KEY ([interval_id]) REFERENCES [nietzschebiography].[interval] ([id]),
    CONSTRAINT [interval_citation$fk_interval_citation_2] FOREIGN KEY ([citation_id]) REFERENCES [nietzschebiography].[citation] ([id])
);


GO
CREATE NONCLUSTERED INDEX [fk_interval_citation_2]
    ON [nietzschebiography].[interval_citation]([citation_id] ASC);

