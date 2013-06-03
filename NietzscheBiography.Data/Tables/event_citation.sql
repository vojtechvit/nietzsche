CREATE TABLE [nietzschebiography].[event_citation] (
    [event_id]    BIGINT NOT NULL,
    [citation_id] BIGINT NOT NULL,
    CONSTRAINT [PK_event_citation_event_id] PRIMARY KEY CLUSTERED ([event_id] ASC, [citation_id] ASC),
    CONSTRAINT [event_citation$fk_event_citation_1] FOREIGN KEY ([event_id]) REFERENCES [nietzschebiography].[event] ([id]),
    CONSTRAINT [event_citation$fk_event_citation_2] FOREIGN KEY ([citation_id]) REFERENCES [nietzschebiography].[citation] ([id])
);


GO
CREATE NONCLUSTERED INDEX [fk_event_citation_2]
    ON [nietzschebiography].[event_citation]([citation_id] ASC);

