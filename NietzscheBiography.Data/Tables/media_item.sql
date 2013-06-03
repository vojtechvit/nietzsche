CREATE TABLE [nietzschebiography].[media_item] (
    [id]                      BIGINT         IDENTITY (991, 1) NOT NULL,
    [original_media_item_id]  BIGINT         DEFAULT (NULL) NULL,
    [composite_media_item_id] BIGINT         DEFAULT (NULL) NULL,
    [media_item_type_id]      INT            DEFAULT (NULL) NULL,
    [title]                   NVARCHAR (200) DEFAULT (NULL) NULL,
    [original_title]          NVARCHAR (200) DEFAULT (NULL) NULL,
    [url]                     NVARCHAR (250) DEFAULT (NULL) NULL,
    [isbn]                    NVARCHAR (24)  DEFAULT (NULL) NULL,
    [comment]                 NVARCHAR (250) DEFAULT (NULL) NULL,
    CONSTRAINT [PK_media_item_id] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [media_item$fk_edition_of_media_item] FOREIGN KEY ([composite_media_item_id]) REFERENCES [nietzschebiography].[media_item] ([id]),
    CONSTRAINT [media_item$fk_media_item_classification] FOREIGN KEY ([media_item_type_id]) REFERENCES [nietzschebiography].[media_item_type] ([id]),
    CONSTRAINT [media_item$fk_part_of_media_item] FOREIGN KEY ([original_media_item_id]) REFERENCES [nietzschebiography].[media_item] ([id])
);


GO
CREATE NONCLUSTERED INDEX [fk_edition_of_media_item]
    ON [nietzschebiography].[media_item]([composite_media_item_id] ASC);


GO
CREATE NONCLUSTERED INDEX [fk_media_item_classification]
    ON [nietzschebiography].[media_item]([media_item_type_id] ASC);


GO
CREATE NONCLUSTERED INDEX [fk_part_of_media_item]
    ON [nietzschebiography].[media_item]([original_media_item_id] ASC);

