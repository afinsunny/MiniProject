const defaultBaseUrl = "https://localhost:7106";

export async function calculateProbability({ type, a, b, baseUrl = defaultBaseUrl }) {
  const response = await fetch(`${baseUrl}/api/Probability/calculate`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ type, a, b })
  });

  const contentType = response.headers.get("content-type") || "";
  const isJson = contentType.includes("application/json");

  if (!response.ok) {
    const message = isJson ? (await response.json()) : (await response.text());
    const errorText = typeof message === "string" ? message : JSON.stringify(message);
    throw new Error(errorText || "Request failed.");
  }

  const data = await response.json();
  return data;
}