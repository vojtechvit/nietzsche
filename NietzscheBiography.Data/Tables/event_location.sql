CREATE TABLE [nietzschebiography].[event_location] (
    [event_id]         BIGINT NOT NULL,
    [location_id]      BIGINT NOT NULL,
    [thematic_role_id] INT    NOT NULL,
    CONSTRAINT [PK_event_location_event_id] PRIMARY KEY CLUSTERED ([event_id] ASC, [location_id] ASC),
    CONSTRAINT [event_location$fk_event_location_1] FOREIGN KEY ([event_id]) REFERENCES [nietzschebiography].[event] ([id]),
    CONSTRAINT [event_location$fk_event_location_2] FOREIGN KEY ([location_id]) REFERENCES [nietzschebiography].[location] ([id]),
    CONSTRAINT [event_location$fk_event_location_thematic_role] FOREIGN KEY ([thematic_role_id]) REFERENCES [nietzschebiography].[thematic_role] ([id])
);


GO
CREATE NONCLUSTERED INDEX [fk_event_location_2]
    ON [nietzschebiography].[event_location]([location_id] ASC);


GO
CREATE NONCLUSTERED INDEX [fk_event_location_thematic_role]
    ON [nietzschebiography].[event_location]([thematic_role_id] ASC);

