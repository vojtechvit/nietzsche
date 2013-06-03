CREATE TABLE [nietzschebiography].[organization_type] (
    [id]    INT           IDENTITY (8, 1) NOT NULL,
    [label] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_organization_type_id] PRIMARY KEY CLUSTERED ([id] ASC)
);

