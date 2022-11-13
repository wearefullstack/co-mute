import React, { useState, useEffect } from "react";

export function useDebounce(value, delay) {
	const [debouncedValue, setDebouncedValue] = useState(value);

	useEffect(() => {
		const timeoutId = setTimeout(() => {
			setDebouncedValue(value);
		}, delay);
		return () => clearTimeout(timeoutId);
	}, [value]);
	return debouncedValue;
}
