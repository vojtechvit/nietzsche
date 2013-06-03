CREATE TABLE [nietzschebiography].[individual] (
    [participant_id] BIGINT         NOT NULL,
    [location_id]    BIGINT         DEFAULT (NULL) NULL,
    [description]    NVARCHAR (MAX) NULL,
    [last_name]      NVARCHAR (50)  DEFAULT (NULL) NULL,
    [given_names]    NVARCHAR (100) DEFAULT (NULL) NULL,
    [name_prefix]    NVARCHAR (20)  DEFAULT (NULL) NULL,
    [name_suffix]    NVARCHAR (20)  DEFAULT (NULL) NULL,
    [gender]         TINYINT   DEFAULT (NULL) NULL,
    [profession]     NVARCHAR (250) DEFAULT (NULL) NULL,
    CONSTRAINT [PK_individual_participant_id] PRIMARY KEY CLUSTERED ([participant_id] ASC),
    CONSTRAINT [individual$fk_individual_nationality] FOREIGN KEY ([location_id]) REFERENCES [nietzschebiography].[country] ([location_id]),
    CONSTRAINT [individual$fk_individual_participant] FOREIGN KEY ([participant_id]) REFERENCES [nietzschebiography].[participant] ([id])
);


GO
CREATE NONCLUSTERED INDEX [fk_individual_nationality]
    ON [nietzschebiography].[individual]([location_id] ASC);

