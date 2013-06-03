CREATE TABLE [nietzschebiography].[citation] (
    [id]            BIGINT         IDENTITY (1402, 1) NOT NULL,
    [media_item_id] BIGINT         NOT NULL,
    [title]         NVARCHAR (100) DEFAULT (NULL) NULL,
    [text]          NVARCHAR (MAX) NULL,
    [standalone]    SMALLINT       NOT NULL,
    CONSTRAINT [PK_citation_id] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [citation$fk_citation_media_item] FOREIGN KEY ([media_item_id]) REFERENCES [nietzschebiography].[media_item] ([id])
);


GO
CREATE NONCLUSTERED INDEX [fk_citation_media_item]
    ON [nietzschebiography].[citation]([media_item_id] ASC);

