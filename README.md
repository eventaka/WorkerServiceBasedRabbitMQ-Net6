# WorkerServiceBasedRabbitMQ

This worker service works in parallel of Rest service called ServiceBasedRabbit.
The worker service is a broker message that the rest service send to it through RabbitMQ.
When the worker has read the message, it saves it in a local SqlServer Database.

It's recommanded to launch first the WorkerServiceBasedRabbitMQ.
Please refer to the Readme file of ServiceBasedRabbit Repository to get all the instructions.

