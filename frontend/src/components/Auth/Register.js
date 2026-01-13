import "./Register.css";
import axios from "axios";
import { useState } from "react";
import { Link } from "react-router-dom";
import { toast } from "react-hot-toast";

export default function Register() {
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
//   const [confirmPassword, setConfirmPassword] = useState("");
  const [role, setRole] = useState("");
//   const [gpa, setGpa] = useState("");
//   const [skills, setSkills] = useState("");
//   const [interests, setInterests] = useState("");
//   const [researchInterests, setResearchInterests] = useState("");
  const [department, setDepartment] = useState("");

  async function handleSubmit(e) {
    e.preventDefault();

    // if (password !== confirmPassword) {
    //   toast.error("Passwords do not match");
    //   return;
    // }

    try {
        const res = await axios.post("https://localhost:7245/api/Auth/register", {
              name: name,
              email: email,
              password: password,
              role: role,
            //   gpa: Number(gpa),
            //   skills: skills,
            //   interests: interests,
            //   researchInterests: researchInterests,
              department: department
        });
        toast.success("Registered Successfully");
        // reset
        setName("");
        setEmail("");
        setPassword("");
        setRole("");
//         setGpa("");
//         setSkills("");
//         setInterests("");
//         setResearchInterests("");
        setDepartment("");
    } catch (error) {
        toast.error("Registration Failed");
        console.error(error);
    }
  }

  return (
    <div className="register-container">
      <form className="register-form" onSubmit={handleSubmit}>
        <h2 className="register-title">Create Account</h2>

        <input
          type="text"
          placeholder="Name"
          value={name}
          onChange={(e) => setName(e.target.value)}
          required
        />

        <input
          type="email"
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          required
        />

        <input
          type="password"
          placeholder="Password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
        />

        {/* <input
          type="password"
          placeholder="Confirm Password"
          value={confirmPassword}
          onChange={(e) => setConfirmPassword(e.target.value)}
          required
        /> */}

        <input
          type="text"
          placeholder="Role"
          value={role}
          onChange={(e) => setRole(e.target.value)}
          required
        />

        {/* <input
          type="text"
          placeholder="GPA"
          value={gpa}
          onChange={(e) => setGpa(e.target.value)}
          required
        /> */}

        {/* <input
          type="text"
          placeholder="Skills"
          value={skills}
          onChange={(e) => setSkills(e.target.value)}
          required
        /> */}

        {/* <input
          type="text"
          placeholder="Interests"
          value={interests}
          onChange={(e) => setInterests(e.target.value)}
          required
        /> */}

        {/* <input
          type="text"
          placeholder="Research Interests"
          value={researchInterests}
          onChange={(e) => setResearchInterests(e.target.value)}
          required
        /> */}

        <input
          type="text"
          placeholder="Department"
          value={department}
          onChange={(e) => setDepartment(e.target.value)}
          required
        />

        <button type="submit">Register</button>

        <p className="register-link">
          Already have an account?{" "}
          <Link to="/login">Login</Link>
        </p>
      </form>
    </div>
  );
}
