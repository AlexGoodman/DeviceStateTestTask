## Test task

### Test solution should have 3 different projects:

- The console application for collecting information from computer and sending it to webService. Connection must be set by webSocket. Information for collecting: Computer name, time zone, OS Name, .net version.
- WebService should track when device goes online and offline, get information from device every 5 minutes and send that information to azure function.
- Azure function for processing information from webService. It should update information only if needed (if we found the difference between current state and previous result) use DB ms sql, for connection - linqToDB