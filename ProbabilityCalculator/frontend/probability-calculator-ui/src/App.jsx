import { useMemo, useState } from "react";
import { calculateProbability } from "./api/probabilityApi";

export default function App() {
  const [a, setA] = useState("");
  const [b, setB] = useState("");
  const [type, setType] = useState("CombinedWith");
  const [result, setResult] = useState(null);
  const [error, setError] = useState("");
  const [isLoading, setIsLoading] = useState(false);

  const canSubmit = useMemo(() => {
    return a.trim().length > 0 && b.trim().length > 0 && type.trim().length > 0 && !isLoading;
  }, [a, b, type, isLoading]);

  async function onCalculate(e) {
    e.preventDefault();
    setError("");
    setResult(null);
    setIsLoading(true);

    try {
      const data = await calculateProbability({ type, a, b });
      setResult(data.result);
    } catch (err) {
      setError(err?.message || "Something went wrong.");
    } finally {
      setIsLoading(false);
    }
  }

  return (
    <div className="page">
      <div className="card">
        <h1 className="title">Probability Calculator</h1>
        <p className="subtitle">Enter two probabilities (0 to 1) and choose a calculation.</p>

        <form className="form" onSubmit={onCalculate}>
          <div className="row">
            <label className="label" htmlFor="a">
              Probability A
            </label>
            <input
              id="a"
              className="input"
              inputMode="decimal"
              placeholder="e.g. 0.5"
              value={a}
              onChange={(e) => setA(e.target.value)}
            />
          </div>

          <div className="row">
            <label className="label" htmlFor="b">
              Probability B
            </label>
            <input
              id="b"
              className="input"
              inputMode="decimal"
              placeholder="e.g. 0.2"
              value={b}
              onChange={(e) => setB(e.target.value)}
            />
          </div>

          <div className="row">
            <label className="label" htmlFor="type">
              Calculation Type
            </label>
            <select id="type" className="select" value={type} onChange={(e) => setType(e.target.value)}>
              <option value="CombinedWith">CombinedWith </option>
              <option value="Either">Either </option>
            </select>
          </div>

          <button className="button" type="submit" disabled={!canSubmit}>
            {isLoading ? "Calculating..." : "Calculate"}
          </button>
        </form>

        {error ? (
          <div className="error" role="alert">
            {error}
          </div>
        ) : null}

        {result !== null ? (
          <div className="result">
            <div className="resultLabel">Result</div>
            <div className="resultValue">{Number.isFinite(result) ? result.toFixed(6) : String(result)}</div>
          </div>
        ) : null}
      </div>
    </div>
  );
}