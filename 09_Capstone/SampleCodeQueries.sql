--Return Venues
SELECT name FROM venue
ORDER by name;

--Return a venue for viewing, may not need to join the state, handle category duplicates
SELECT v.name, ci.name, ci.state_abbreviation, ca.name, v.description FROM venue v
JOIN city ci ON v.city_id = ci.id
JOIN state s ON ci.state_abbreviation = s.abbreviation
LEFT JOIN category_venue cv ON v.id = cv.venue_id
LEFT JOIN category ca ON cv.category_id = ca.id
WHERE v.name = 'Painted Squirrel Club';

--Return all reservations
SELECT r.reservation_id, v.name venue_name, s.name space_name, s.id space_id, r.reserved_for, r.number_of_attendees, r.start_date, r.end_date, s.daily_rate FROM reservation r
JOIN space s ON r.space_id = s.id
JOIN venue v ON s.venue_id = v.id

--Return spaces for a venue, replace the venue in where statement
SELECT * FROM space
WHERE venue_id = 1;

--Return reservations not between those dates (THIS IS A TEST, WILL NOT BE IMPLEMENTED IN CODE)
SELECT * FROM reservation
WHERE start_date NOT BETWEEN '2020-06-15' AND '2020-07-03' AND end_date NOT BETWEEN '2020-06-15' AND '2020-07-03'

--Returns spaces in a venue that have a high enough max occupency and have no bookings between the dates, dates and venue will need to be paremeterized
SELECT TOP 5 id, name, daily_rate, max_occupancy, is_accessible FROM space
WHERE venue_id = 2 AND max_occupancy >= 30 AND (open_from IS NULL OR open_from <= 8
AND id NOT IN(SELECT space_id FROM reservation WHERE (start_date BETWEEN '2020-06-15' AND '2020-06-18') OR (end_date BETWEEN '2020-06-15' AND '2020-06-18') 
OR (start_date < '2020-06-15' AND end_date > '2020-06-18'));

--Inserts a reservation to the database (reservation_id is the identity so cannot insert into table with out setting identity insert to on)
BEGIN TRANSACTION;

INSERT INTO reservation (space_id, number_of_attendees, start_date, end_date, reserved_for)
VALUES (40, 60, '2020-08-01', '2020-08-15', 'Test Reservation string')

ROLLBACK TRANSACTION