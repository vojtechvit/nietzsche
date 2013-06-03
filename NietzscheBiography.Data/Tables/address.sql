CREATE TABLE [nietzschebiography].[address] (
    [location_id]        BIGINT         NOT NULL,
    [populated_place_id] BIGINT         NOT NULL,
    [postal_code]        NVARCHAR (10)  DEFAULT (NULL) NULL,
    [street_name]        NVARCHAR (100) DEFAULT (NULL) NULL,
    [building_number]    NVARCHAR (10)  DEFAULT (NULL) NULL,
    [entrance]           NVARCHAR (10)  DEFAULT (NULL) NULL,
    [floor]              SMALLINT       DEFAULT (NULL) NULL,
    [apartment_number]   NVARCHAR (10)  DEFAULT (NULL) NULL,
    CONSTRAINT [PK_address_location_id] PRIMARY KEY CLUSTERED ([location_id] ASC),
    CONSTRAINT [address$fk_address_location] FOREIGN KEY ([location_id]) REFERENCES [nietzschebiography].[location] ([id]),
    CONSTRAINT [address$fk_address_populated_place] FOREIGN KEY ([populated_place_id]) REFERENCES [nietzschebiography].[populated_place] ([location_id])
);


GO
CREATE NONCLUSTERED INDEX [fk_address_populated_place]
    ON [nietzschebiography].[address]([populated_place_id] ASC);

