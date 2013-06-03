CREATE TABLE [nietzschebiography].[media_item_type] (
    [id]    INT           IDENTITY (19, 1) NOT NULL,
    [label] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_media_item_type_id] PRIMARY KEY CLUSTERED ([id] ASC)
);

