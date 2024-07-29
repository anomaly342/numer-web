import CalcRequest from "./types";

const getResult = async (calcObj: CalcRequest, api: string) => {
	const response = await fetch(
		`${process.env.NEXT_PUBLIC_SERVER_URL}/methods/${api}`,
		{
			method: "POST",
			mode: "cors",
			headers: {
				"Content-Type": "application/json",
			},
			body: JSON.stringify(calcObj),
		}
	);

	if (response.ok) {
		const data = await response.json();
		return data;
	}
};

export default getResult;
