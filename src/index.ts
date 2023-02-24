import express, { Express, Request, Response } from 'express';

const app: Express = express();
const port = process.env.PORT || 8080;

app.get('/', async (req: Request, res: Response) => {
  console.log(req.url)
  await fetch("https://vg.no")
  res.send('Express + TypeScript Server');
});

app.get('/feil', async (req: Request, res: Response) => {
  console.log(req.url)
  try {
    await fetch("https://vgdafdasfdasfdas.no")
  } catch {
    res.status(500).send("Error!")
  }
});


app.listen(port, () => {
  console.log(`⚡️[server]: Server is running at http://localhost:${port}`);
});