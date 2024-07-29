"use client";

import { MouseEvent, useState, useRef, useEffect, FormEvent } from "react";
import { useQuery } from "@tanstack/react-query";
import { Montserrat } from "next/font/google";
import Image from "next/image";
import dropdown_svg from "@/assets/dropdown_arrow.svg";
import endPoints from "@/utils/endPoints";
import CalcRequest from "@/utils/types";
import getResult from "@/utils/fetch";
const montserrat = Montserrat({ weight: "700", subsets: ["latin"] });

export default function Home() {
	const [isShown, setIsShown] = useState(false);
	const [method, setMethod] = useState(0);
	const dropdown_menu = useRef<any>(null);
	const base_menu = useRef<any>(null);
	const object: Partial<CalcRequest> = {};

	const { data, isError, isLoading, refetch, isFetched } = useQuery({
		queryKey: ["result"],
		queryFn: () => getResult(object as CalcRequest, endPoints[method].api),
		enabled: false,
		refetchOnWindowFocus: false,
	});

	const onClick = (e: MouseEvent<HTMLAnchorElement>) => {
		e.preventDefault();

		setIsShown((prev) => !prev);
	};

	const onSubmit = (e: FormEvent<HTMLFormElement>) => {
		e.preventDefault();

		const formData = new FormData(e.currentTarget);

		formData.forEach(function (value, key) {
			object[key as keyof CalcRequest] = Number(value);
		});

		refetch();
	};

	useEffect(() => {
		/**
		 * Invoke Function onClick outside of element
		 */
		function handleClickOutside(this: HTMLElement, e: Event) {
			if (
				dropdown_menu &&
				!dropdown_menu.current.contains(e.target) &&
				!base_menu.current.contains(e.target)
			) {
				setIsShown(false);
			}
		}
		// Bind
		document.addEventListener("mousedown", handleClickOutside);
		return () => {
			// dispose
			document.removeEventListener("mousedown", handleClickOutside);
		};
	}, []);

	return (
		<>
			<header className="p-5 w-full place-content-center grid bg-white">
				<h1
					className={`${montserrat.className} text-3xl inline text-[#51279B]`}
				>
					Numerical Calculator
				</h1>
			</header>
			<main className="py-6 px-4 grid place-content-center">
				<form className="max-w-3xl" onSubmit={onSubmit}>
					<div className="bg-white text-xl p-6 shadow-md ">
						<label htmlFor="findee" className="mb-1 block">
							Number
						</label>
						<input
							className="border-solid border-[#653CAD] border-b-2 w-48 mb-5"
							type="number"
							name="findee"
							id="findee"
							defaultValue="34"
							required
						/>
						<label htmlFor="n" className="mb-1 mt-4 block">
							Nth Root
						</label>
						<input
							className="border-solid border-[#653CAD] border-b-2 w-48"
							type="number"
							name="n"
							id="n"
							defaultValue={3}
							required
						/>
					</div>
					<h4 className="text-[#627D98] font-bold text-sm mt-6">Boundary</h4>
					<div className="flex mt-3 text-lg justify-between">
						<div className="bg-white text-xl p-6 shadow-md basis-1/3 text-center">
							<label htmlFor="a" className="">
								Start
							</label>
							<input
								type="number"
								name="a"
								id="a"
								className="border-solid border-[#653CAD] border-b-2 w-full text-center "
								defaultValue={1}
								required
							/>
						</div>
						<div className="bg-white text-xl p-6 shadow-md basis-1/3 text-center">
							<label htmlFor="b" className="">
								End
							</label>
							<input
								type="number"
								name="b"
								id="b"
								className="border-solid border-[#653CAD] border-b-2 w-full text-center"
								defaultValue={10}
								required
							/>
						</div>
					</div>
					<h4 className="text-[#627D98] font-bold text-sm mt-6 ">Method</h4>
					<a
						href="#"
						className={`p-6 bg-white relative shadow-md w-full mt-3 flex justify-between ${
							isShown ? "" : "hover:bg-[#f5f5f5]"
						} `}
						onClick={onClick}
						ref={base_menu}
					>
						<p>{endPoints[method].name}</p>
						<Image src={dropdown_svg} alt="dropdown" className=""></Image>
						<ul
							ref={dropdown_menu}
							className={`${
								isShown
									? "absolute left-0 top-[100%]  bg-white w-full shadow-md"
									: "hidden"
							}`}
						>
							{endPoints.map((e, i) => (
								<li
									key={i}
									className="p-6 hover:bg-[#f5f5f5]"
									data-choice={i}
									onClick={() => setMethod(i)}
								>
									{e.name}
								</li>
							))}
						</ul>
					</a>
					<div className="bg-white p-6 mt-3 shadow-md basis-1/3 flex justify-between">
						<label htmlFor="maxIteration" className="flex-shrink-0">
							Maximum Iterations
						</label>
						<input
							type="number"
							name="maxIteration"
							id="maxIteration"
							defaultValue={100000}
							className="border-solid border-[#653CAD] border-b-2 w-36 text-center"
							required
						/>
					</div>
					<button
						type="submit"
						className="mt-9 p-3 bg-[#F9703E] text-white text-lg font-bold w-full"
					>
						Submit
					</button>
				</form>
				{isFetched && !isError && <p className="mt-10">{data?.value}</p>}
			</main>
		</>
	);
}
