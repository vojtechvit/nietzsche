CREATE TABLE [nietzschebiography].[event_participant] (
    [event_id]         BIGINT NOT NULL,
    [participant_id]   BIGINT NOT NULL,
    [thematic_role_id] INT    NOT NULL,
    CONSTRAINT [PK_event_participant_event_id] PRIMARY KEY CLUSTERED ([event_id] ASC, [participant_id] ASC, [thematic_role_id] ASC),
    CONSTRAINT [event_participant$fk_event_participant_1] FOREIGN KEY ([event_id]) REFERENCES [nietzschebiography].[event] ([id]),
    CONSTRAINT [event_participant$fk_event_participant_2] FOREIGN KEY ([participant_id]) REFERENCES [nietzschebiography].[participant] ([id]),
    CONSTRAINT [event_participant$fk_event_participant_thematic_role] FOREIGN KEY ([thematic_role_id]) REFERENCES [nietzschebiography].[thematic_role] ([id])
);


GO
CREATE NONCLUSTERED INDEX [fk_event_participant_2]
    ON [nietzschebiography].[event_participant]([participant_id] ASC);


GO
CREATE NONCLUSTERED INDEX [fk_event_participant_thematic_role]
    ON [nietzschebiography].[event_participant]([thematic_role_id] ASC);

