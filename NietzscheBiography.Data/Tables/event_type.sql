CREATE TABLE [nietzschebiography].[event_type] (
    [id]                INT           IDENTITY (93, 1) NOT NULL,
    [event_category_id] INT           DEFAULT (NULL) NULL,
    [label]             NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_event_type_id] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [event_type$fk_event_type_categorization] FOREIGN KEY ([event_category_id]) REFERENCES [nietzschebiography].[event_category] ([id])
);


GO
CREATE NONCLUSTERED INDEX [fk_event_type_categorization]
    ON [nietzschebiography].[event_type]([event_category_id] ASC);

