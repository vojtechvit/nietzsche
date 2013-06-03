CREATE TABLE [nietzschebiography].[country] (
    [location_id]          BIGINT         NOT NULL,
    [name]                 NVARCHAR (150) NOT NULL,
    [fips104_code]         NCHAR (2)      DEFAULT (NULL) NULL,
    [iso2_code]            NCHAR (2)      DEFAULT (NULL) NULL,
    [iso3_code]            NCHAR (3)      DEFAULT (NULL) NULL,
    [ison_code]            SMALLINT       DEFAULT (NULL) NULL,
    [iana_code]            NCHAR (2)      DEFAULT (NULL) NULL,
    [nationality_singular] NVARCHAR (50)  DEFAULT (NULL) NULL,
    [nationality_plural]   NVARCHAR (50)  DEFAULT (NULL) NULL,
    CONSTRAINT [PK_country_location_id] PRIMARY KEY CLUSTERED ([location_id] ASC),
    CONSTRAINT [country$fk_country_location] FOREIGN KEY ([location_id]) REFERENCES [nietzschebiography].[location] ([id])
);

