import type { Handle } from "@sveltejs/kit";
import { env } from '$env/dynamic/private'

const ADMIN_LOGIN=env.ADMIN_LOGIN || "future:frictions"

export const handle: Handle = async ({ event, resolve }) => {
    const url = new URL(event.request.url);

    if (!url.pathname.startsWith("/api")) {
        console.log("Admin area")
        const auth = event.request.headers.get("Authorization");

        if (auth !== `Basic ${btoa(ADMIN_LOGIN)}`) {
            return new Response("Not authorized", {
                status: 401,
                headers: {
                    "WWW-Authenticate":
                        'Basic realm="User Visible Realm", charset="UTF-8"',
                },
            });
        }
    } else {
        console.log("Public area")
    }

	const response = await resolve(event);
	return response;
};
