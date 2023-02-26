import http from "k6/http";

export const options = {
  vus: 100,
  iterations: 10000,
};

export default function () {
  http.get("http://localhost:5065/todo");
  http.get("http://localhost:5065/todoError");
}
