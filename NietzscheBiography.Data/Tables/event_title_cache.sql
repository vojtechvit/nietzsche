CREATE TABLE [nietzschebiography].[event_title_cache]
(
	[event_id] BIGINT NOT NULL,
    [title] TEXT NOT NULL,
	CONSTRAINT [PK_event_title_cache_id] PRIMARY KEY CLUSTERED ([event_id] ASC),
    CONSTRAINT [FK_event_title_cache_event] FOREIGN KEY ([event_id]) REFERENCES [nietzschebiography].[event]([id])
)
