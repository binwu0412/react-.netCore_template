const rabbitmqService = require('./rabbitmqService');

const express = require('express');
const app = express();
const port = 8002;

const cors = require('cors');
const corsOptions = {
  origin: 'http://localhost:3000'
}

app.use(cors(corsOptions));
rabbitmqService.getInstance()
  .then(broker => 
    broker.subscribe('test2', (msg, ack) => {
      console.log('Message:', msg.content.toString())
      ack();
    }))
  .catch(err => console.log('Rabbitmq connection error!'));

app.get('/', (req, res) => {
  res.send('Hello World!');
});

app.get('/sendTestmsg', (req, res) => {
  rabbitmqService.getInstance()
    .then(broker =>
      broker.send('test2', Buffer.from('This is a test message')))
    .then(() => res.send('Message sent!'))
    .catch(err => {
      console.log('failed to publish message', err);
      res.status(500).send();
    });
});

app.listen(port, () => {
  console.log(`Example app listening at http://localhost:${port}`)
});

