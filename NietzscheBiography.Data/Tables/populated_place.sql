CREATE TABLE [nietzschebiography].[populated_place] (
    [location_id] BIGINT        NOT NULL,
    [country_id]  BIGINT        DEFAULT (NULL) NULL,
    [name]        NVARCHAR (50) NOT NULL,
    [time_zone]   NCHAR (6)     DEFAULT (NULL) NULL,
    CONSTRAINT [PK_populated_place_location_id] PRIMARY KEY CLUSTERED ([location_id] ASC),
    CONSTRAINT [populated_place$fk_populated_place_country] FOREIGN KEY ([country_id]) REFERENCES [nietzschebiography].[country] ([location_id]),
    CONSTRAINT [populated_place$fk_populated_place_location] FOREIGN KEY ([location_id]) REFERENCES [nietzschebiography].[location] ([id])
);


GO
CREATE NONCLUSTERED INDEX [fk_populated_place_country]
    ON [nietzschebiography].[populated_place]([country_id] ASC);

