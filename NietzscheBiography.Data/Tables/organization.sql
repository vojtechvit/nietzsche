CREATE TABLE [nietzschebiography].[organization] (
    [participant_id]       BIGINT         NOT NULL,
    [location_id]          BIGINT         DEFAULT (NULL) NULL,
    [organization_type_id] INT            NOT NULL,
    [description]          NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_organization_participant_id] PRIMARY KEY CLUSTERED ([participant_id] ASC),
    CONSTRAINT [organization$fk_organization_classification] FOREIGN KEY ([organization_type_id]) REFERENCES [nietzschebiography].[organization_type] ([id]),
    CONSTRAINT [organization$fk_organization_establishment] FOREIGN KEY ([location_id]) REFERENCES [nietzschebiography].[location] ([id]),
    CONSTRAINT [organization$fk_organization_participant] FOREIGN KEY ([participant_id]) REFERENCES [nietzschebiography].[participant] ([id])
);


GO
CREATE NONCLUSTERED INDEX [fk_organization_classification]
    ON [nietzschebiography].[organization]([organization_type_id] ASC);


GO
CREATE NONCLUSTERED INDEX [fk_organization_establishment]
    ON [nietzschebiography].[organization]([location_id] ASC);

