import http from "k6/http";
import { sleep } from "k6";

export const options = {
  vus: 10,
  iterations: 10,
};

export default function () {
  http.get("https://test.k6.io");
  sleep(1);
}