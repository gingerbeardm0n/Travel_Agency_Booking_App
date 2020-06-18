SELECT name FROM venue
ORDER by name;

--Return a venue for viewing
SELECT TOP 1 v.name, ci.name, ci.state_abbreviation, ca.name, v.description FROM venue v
JOIN city ci ON v.city_id = ci.id
JOIN state s ON ci.state_abbreviation = s.abbreviation
JOIN category_venue cv ON v.id = cv.venue_id
JOIN category ca ON cv.category_id = ca.id
WHERE v.name = 'Hidden Owl Eatery';

--Return all reservations
SELECT r.reservation_id, v.name, s.name, r.reserved_for, r.number_of_attendees, r.start_date, r.end_date, s.daily_rate FROM reservation r
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
WHERE venue_id = 2 AND max_occupancy >= 30 
AND id NOT IN(SELECT space_id FROM reservation WHERE (start_date BETWEEN '2020-06-15' AND '2020-07-03') OR (end_date BETWEEN '2020-06-15' AND '2020-07-03'))