import "../components/Auth/Register.css";
import { Link } from "react-router-dom";

export default function NotFound() {
  return (
    <div className="register-container">
      <div className="register-form" style={{ textAlign: "center" }}>
        <h2 className="register-title">404 - Page Not Found</h2>

        <p style={{ marginBottom: "20px" }}>
          The page you are looking for does not exist.
        </p>

        <Link style={{textDecoration: "none", color: "blue"}} to="/login" className="register-link">
          Go to Login Page
        </Link>
      </div>
    </div>
  );
}
