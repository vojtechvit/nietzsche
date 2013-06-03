CREATE TABLE [nietzschebiography].[event] (
    [id]                   BIGINT         IDENTITY (2618, 1) NOT NULL,
    [event_type_id]        INT            NOT NULL,
    [description]          NVARCHAR (250) DEFAULT (NULL) NULL,
    [start_date]           DATE           DEFAULT (NULL) NULL,
    [end_date]             DATE           DEFAULT (NULL) NULL,
    [start_date_precision] TINYINT   DEFAULT (NULL) NULL,
    [end_date_precision]   TINYINT   DEFAULT (NULL) NULL,
    [importance]           INT            DEFAULT (NULL) NULL,
    CONSTRAINT [PK_event_id] PRIMARY KEY NONCLUSTERED ([id] ASC),
    CONSTRAINT [event$fk_event_classification] FOREIGN KEY ([event_type_id]) REFERENCES [nietzschebiography].[event_type] ([id])
);
GO

CREATE NONCLUSTERED INDEX [fk_event_classification]
    ON [nietzschebiography].[event]([event_type_id] ASC);
GO

CREATE CLUSTERED INDEX [IX_event_start_date_end_date] ON [nietzschebiography].[event] ([start_date], [end_date])
GO

CREATE NONCLUSTERED INDEX [IX_event_importance] ON [nietzschebiography].[event] ([importance])
GO