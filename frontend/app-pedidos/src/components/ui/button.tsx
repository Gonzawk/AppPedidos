import React from "react";
import { cn } from "@/lib/utils"; // Asegúrate de tener esta utilidad o definir una similar

const buttonVariants = {
  default: "bg-blue-600 text-white hover:bg-blue-700",
  outline: "border border-gray-300 text-gray-700 hover:bg-gray-100",
};

type ButtonProps = React.ButtonHTMLAttributes<HTMLButtonElement> & {
  variant?: keyof typeof buttonVariants;
};

export const Button: React.FC<ButtonProps> = ({
  children,
  variant = "default",
  className,
  ...props
}) => {
  return (
    <button
      {...props}
      className={cn(
        "py-2 px-4 rounded transition font-semibold",
        buttonVariants[variant],
        className
      )}
    >
      {children}
    </button>
  );
};
