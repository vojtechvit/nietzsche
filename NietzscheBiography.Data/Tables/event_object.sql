CREATE TABLE [nietzschebiography].[event_object] (
    [event_id]      BIGINT NOT NULL,
    [media_item_id] BIGINT NOT NULL,
    CONSTRAINT [PK_event_object_event_id] PRIMARY KEY CLUSTERED ([event_id] ASC, [media_item_id] ASC),
    CONSTRAINT [event_object$fk_event_object_1] FOREIGN KEY ([event_id]) REFERENCES [nietzschebiography].[event] ([id]),
    CONSTRAINT [event_object$fk_event_object_2] FOREIGN KEY ([media_item_id]) REFERENCES [nietzschebiography].[media_item] ([id])
);


GO
CREATE NONCLUSTERED INDEX [fk_event_object_2]
    ON [nietzschebiography].[event_object]([media_item_id] ASC);

