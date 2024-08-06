CREATE TABLE IF NOT EXISTS CleaningResults (
      ID SERIAL PRIMARY KEY,
      Commands integer,
      Result integer,
      Timestamp timestamp,
      Duration double precision
)