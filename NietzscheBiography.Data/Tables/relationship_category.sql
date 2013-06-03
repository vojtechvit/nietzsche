CREATE TABLE [nietzschebiography].[relationship_category] (
    [id]    INT           IDENTITY (8, 1) NOT NULL,
    [label] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_relationship_category_id] PRIMARY KEY CLUSTERED ([id] ASC)
);

