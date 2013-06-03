CREATE TABLE [nietzschebiography].[relationship] (
    [id]                           BIGINT IDENTITY (294, 1) NOT NULL,
    [determinant_participant_id]   BIGINT NOT NULL,
    [immanent_participant_id]      BIGINT NOT NULL,
    [relationship_type_id]         INT    NOT NULL,
    [inverse_relationship_type_id] INT    NOT NULL,
    CONSTRAINT [PK_relationship_id] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [relationship$fk_determinant_participant] FOREIGN KEY ([immanent_participant_id]) REFERENCES [nietzschebiography].[participant] ([id]),
    CONSTRAINT [relationship$fk_immanent_participant] FOREIGN KEY ([determinant_participant_id]) REFERENCES [nietzschebiography].[participant] ([id]),
    CONSTRAINT [relationship$fk_inverse_relationship_classification] FOREIGN KEY ([inverse_relationship_type_id]) REFERENCES [nietzschebiography].[relationship_type] ([id]),
    CONSTRAINT [relationship$fk_relationship_classification] FOREIGN KEY ([relationship_type_id]) REFERENCES [nietzschebiography].[relationship_type] ([id])
);


GO
CREATE NONCLUSTERED INDEX [fk_determinant_participant]
    ON [nietzschebiography].[relationship]([immanent_participant_id] ASC);


GO
CREATE NONCLUSTERED INDEX [fk_immanent_participant]
    ON [nietzschebiography].[relationship]([determinant_participant_id] ASC);


GO
CREATE NONCLUSTERED INDEX [fk_inverse_relationship_classification]
    ON [nietzschebiography].[relationship]([inverse_relationship_type_id] ASC);


GO
CREATE NONCLUSTERED INDEX [fk_relationship_classification]
    ON [nietzschebiography].[relationship]([relationship_type_id] ASC);

