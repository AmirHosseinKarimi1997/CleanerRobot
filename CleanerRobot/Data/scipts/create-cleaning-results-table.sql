CREATE TABLE IF NOT EXISTS CleaningResults (
      ID SERIAL PRIMARY KEY,
      Timestamp timestamp,
      Commands integer,
      Result integer,
      Duration double precision
)