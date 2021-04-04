import axios from "axios";

const env = process.env.NODE_ENV;
const baseURL = env == "development" ? "http://localhost:54240/api" : "";

let httpClient = axios.create({
  baseURL: baseURL,
  timeout: 20000,
  headers: {
    Accept: "application/json",
    "Content-Type": "application/json"
  }
});

export default httpClient;