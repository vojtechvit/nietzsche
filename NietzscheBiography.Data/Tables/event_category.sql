CREATE TABLE [nietzschebiography].[event_category] (
    [id]    INT           IDENTITY (5, 1) NOT NULL,
    [label] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_event_category_id] PRIMARY KEY CLUSTERED ([id] ASC)
);

