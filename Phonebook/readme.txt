Written in C#, .Net Core, with Razor and JQuery, Javascript and Web API.  In Visual Studio 2017.

If you load it into Visual Studio 2017, and hit the play button in IIS Express, it should be 
all that's required to run the website.

I had previous completed a hackajob code challenge, also associated with telephone numbers,
so I used that code as a basis for this.  The original project displayed the names and telephones
within a web browser, allowing for the searching and ordering of names.

The generation of the customer data is based upon predictable random numbers, if the seed
number is the same, the generated output data will be consistent.  To change the data, simply
change the seed number.

Tests are implemented with NUNIT.

Usage/Specification;
-------------------+

All interaction with the web service take place using JSON.

To get all customers and telephone numbers call the following endpoint;

{website}/api/customers

To get all customer phone numbers call the following endpoint, where X
indicates the unique customer_id integer number;

{website}/api/customers/X

To toggle a phone numbers to an active state, call the following endpoint, where X
indicates the unique customer_id integer, and Y indicates the phone number id (unique only
to the specific customer);

{website}/api/customers/X/Y

Modifications and alternative implementations;
---------------------------------------------+

In this implementation I nested the phone numbers within the customers class,
thereby creating a more formal link between the two.

Given a database, this implementation would be better suited to being separated,
with the phone numbers for all customers given within a separate table/array structure, with
some sort of mapping between the two (something like a hashtable, or using linq-to-sql to
do the hard work of the database joins)  For performance later down the line, this alternative
maybe the better solution.

This current implementation, because of how I generated the customer data is currently
fixed to needing both customer_id and number_id, as a key, rather than a single unique
number_id (that would map back to customer_id).

The function to activate a phone number is very basic, and could also be expanded into
a activate/deactivate toggle function; but I would need to have a conversation with the client
if this was a needed function.

I choose to have a front-end interface, because often when interacting with a client, it's
best for them to see something, to guide development further.  API often means nothing
to the non-technical.

The API currently does not have any security implemented, again this would need to be discussed
with the client further.
