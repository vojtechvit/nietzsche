CREATE TABLE [nietzschebiography].[location] (
    [id]        BIGINT        IDENTITY (39644, 1) NOT NULL,
	[geo_location] [sys].[geography] NULL,
    [type]      TINYINT NOT NULL,
    CONSTRAINT [PK_location_id] PRIMARY KEY CLUSTERED ([id] ASC)
);

GO

CREATE SPATIAL INDEX [SPATIAL_location_geo_location] ON [nietzschebiography].[location] ([geo_location])
