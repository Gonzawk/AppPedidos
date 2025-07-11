import React from "react";

export const Input = ({ ...props }: React.InputHTMLAttributes<HTMLInputElement>) => {
  return (
    <input
      {...props}
      className="border p-2 rounded w-full focus:outline-none focus:ring"
    />
  );
};
