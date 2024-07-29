import type { Metadata } from "next";
import { Inter } from "next/font/google";
import ReactQueryProvider from "@/utils/ReactQueryProvider";
import "./globals.css";

const inter = Inter({ subsets: ["latin"], weight: ["400", "700"] });

export const metadata: Metadata = {
	title: "Numer",
	description: "Numerical Calculator",
};

export default function RootLayout({
	children,
}: Readonly<{
	children: React.ReactNode;
}>) {
	return (
		<html lang="en">
			<body className={inter.className + " bg-[#F0F4F8] grid"}>
				<ReactQueryProvider>{children}</ReactQueryProvider>
			</body>
		</html>
	);
}
