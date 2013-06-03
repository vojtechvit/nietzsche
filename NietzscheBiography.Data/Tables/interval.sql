CREATE TABLE [nietzschebiography].[interval] (
    [id]                  BIGINT         IDENTITY (64, 1) NOT NULL,
    [initiating_event_id] BIGINT         NOT NULL,
    [concluding_event_id] BIGINT         NOT NULL,
    [description]         NVARCHAR (250) DEFAULT (NULL) NULL,
    CONSTRAINT [PK_interval_id] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [interval$fk_concluding_event] FOREIGN KEY ([initiating_event_id]) REFERENCES [nietzschebiography].[event] ([id]),
    CONSTRAINT [interval$fk_initiating_event] FOREIGN KEY ([concluding_event_id]) REFERENCES [nietzschebiography].[event] ([id])
);


GO
CREATE NONCLUSTERED INDEX [fk_concluding_event]
    ON [nietzschebiography].[interval]([initiating_event_id] ASC);


GO
CREATE NONCLUSTERED INDEX [fk_initiating_event]
    ON [nietzschebiography].[interval]([concluding_event_id] ASC);

