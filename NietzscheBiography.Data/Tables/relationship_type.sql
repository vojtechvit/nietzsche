CREATE TABLE [nietzschebiography].[relationship_type] (
    [id]                       INT           IDENTITY (39, 1) NOT NULL,
    [relationship_category_id] INT           DEFAULT (NULL) NULL,
    [label]                    NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_relationship_type_id] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [relationship_type$fk_relationship_type_categorization] FOREIGN KEY ([relationship_category_id]) REFERENCES [nietzschebiography].[relationship_category] ([id])
);


GO
CREATE NONCLUSTERED INDEX [fk_relationship_type_categorization]
    ON [nietzschebiography].[relationship_type]([relationship_category_id] ASC);

